import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert, FormFeedback } from 'reactstrap';

import BlockTargetCountryFormGroup from '../../../common/components/BlockTargetCountryFormGroup';
import MultipleSelect from '../../../../components/multiple-select';

import { setSelectedLandingPageId, getLandingPageDetail, fetchData, insertLandingPage, updateLandingPage, toggleLoader, getTargetCountryListByLandingPage } from '../actions/index';
import { defaultState, headerText, globalResource, messages, restrictedCharactersPattern, maxLength, filterDealMode } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { getBlockTargetCountryList, checkBlockAllTargetCountries } from '../../../common/modules/utils/targetcountry';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { handleChangeMultipleSelected } from '../../../../modules/utils';

export class InsertUpdateForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            ...defaultState
        }
    }

    componentDidMount() {
        const { gridData } = this.props;

        !gridData && this.props.fetchData();
    }

    componentDidUpdate(prevProps) {
        const { selectedLandingPageId } = this.props;
        if (selectedLandingPageId !== null && prevProps.selectedLandingPageId !== selectedLandingPageId) {
            this.setState({ isInsert: selectedLandingPageId === defaultInsertId });

            this._handleUpdateState(selectedLandingPageId);
        }
    }

    _handleUpdateState = async (pageId) => {
        this.props.toggleLoader(true);

        if (pageId === 0) {
            const visibleTargetCountryList = await this.props.getTargetCountryListByLandingPage({ pageId });

            this.setState({
                isInsert: true,
                selectedLandingPage:
                {
                    pageName: '',
                    pageCode: '',
                    blockTargetCountry: '',
                    isMobile: false,
                    isExcluded: filterDealMode.Exclude,
                    dealIds: ''
                },
                dealList: this.props.dealList,
                targetCountryList: visibleTargetCountryList
            });
        } else {
            const landingPage = await this.props.getLandingPageDetail(pageId);

            if (landingPage) {
                const pageName = htmlDecode(landingPage.PageName);
                const pageCode = htmlDecode(landingPage.PageCode);
                const dealIds = htmlDecode(landingPage.DealIds);
                const isMobile = landingPage.IsMobile;
                const isExcluded = landingPage.IsExcluded;

                const targetCountryList = await this.props.getTargetCountryListByLandingPage({ pageId });
                const newDealList = handleChangeMultipleSelected(this.props.dealList, dealIds);

                const selectedLandingPage = {
                    pageName,
                    pageCode,
                    isMobile,
                    isExcluded,
                    dealIds,
                    blockTargetCountry: ''
                };

                const newState = {
                    isInsert: false,
                    selectedLandingPage,
                    dealList: newDealList
                };

                if (targetCountryList?.length) {
                    const blockTargetCountryList = getBlockTargetCountryList(targetCountryList);
                    const isBlockAllTargetCountries = checkBlockAllTargetCountries(targetCountryList, blockTargetCountryList);

                    newState.selectedLandingPage.blockTargetCountry = blockTargetCountryList.join(',');
                    newState.targetCountryList = targetCountryList;
                    newState.isBlockAllTargetCountries = isBlockAllTargetCountries;
                }

                this.setState(newState);
            }

        }

        this.props.toggleLoader(false);
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedLandingPage } = this.state;
        const newPage = Object.assign({}, selectedLandingPage, { [target.name]: value });

        this.setState({ selectedLandingPage: newPage });
    }

    handleChangeTargetCountry = (isBlockAllTargetCountries, blockTargetCountry, newTargetCountryList) => {
        const { selectedLandingPage } = this.state;
        const newPage = Object.assign({}, selectedLandingPage, { blockTargetCountry });

        this.setState({ selectedLandingPage: newPage, targetCountryList: newTargetCountryList, isBlockAllTargetCountries });
    }

    _handleChangeFilterDealMode = (event) => {
        const { value } = event.target;
        this.setState({
            selectedLandingPage: {
                ...this.state.selectedLandingPage,
                isExcluded: Number(value),
                dealIds: ''
            },
            dealList: handleChangeMultipleSelected(this.state.dealList, '')
        });
    }

    _handleChangeDeals = (item, selectedDeals) => {
        const isAllDeals = selectedDeals.split(',').length === this.props.dealList.length;

        this.setState({
            selectedLandingPage: { ...this.state.selectedLandingPage, dealIds: isAllDeals ? '0' : selectedDeals },
            dealList: handleChangeMultipleSelected(this.state.dealList, selectedDeals)
        });
    }

    _validatePageCode() {
        const { pageCode } = this.state.selectedLandingPage;
        const hasRestrictedCharacter = restrictedCharactersPattern.test(pageCode);
        const result = {
            isValid: !!pageCode && !hasRestrictedCharacter && pageCode.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !pageCode ? messages.requiredErrorMessage :
                (hasRestrictedCharacter ?
                    'Special characters are not allowed' :
                    messages.maxLengthErrorMessage);
        }

        return result;
    }

    _validatePageName() {
        const { pageName } = this.state.selectedLandingPage;

        const result = {
            isValid: !!pageName && pageName.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !pageName ? messages.requiredErrorMessage : messages.maxLengthErrorMessage;
        }

        return result;
    }

    _handleSubmit = async (isValid) => {
        this.props.toggleLoader(true);
        this.setState({ isClickedSubmit: true });

        if (isValid) {
            const { selectedLandingPage, isInsert } = this.state;
            const { selectedLandingPageId } = this.props;
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";
            const landingPageInfo = Object.assign({}, { pageId: selectedLandingPageId, ...selectedLandingPage });

            const validateMessage = this._validateSelectedDealsByMode(selectedLandingPage.isExcluded, selectedLandingPage.dealIds);
            if (validateMessage) {
                this._handleShowAlert(false, validateMessage);
            } else {
                const result = isInsert ? await this.props.insertLandingPage(landingPageInfo) : await this.props.updateLandingPage(landingPageInfo);

                if (result) {
                    this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
                } else {
                    this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
                }
            }
        }

        this.props.toggleLoader(false);
    }

    _validateSelectedDealsByMode = (isExcludeDeal, selectedDeals) => {
        if (isExcludeDeal === filterDealMode.Exclude && selectedDeals === '0') {
            return 'Selecting all options is not allowed. Please unselect at least one.';
        }

        if (isExcludeDeal === filterDealMode.Include && selectedDeals === '') {
            return 'Please select at least one deal.';
        }

        return '';
    }

    _handleKeyPress = (event, isValid) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit(isValid);
        }
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage(isSuccess), 3000));
    }

    _hideMessage = (isSuccess) => {
        this.setState({
            isShowMessage: false
        });

        if (isSuccess) {
            this.props.fetchData();
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedLandingPageId(null);

        this.setState({ ...defaultState });
    }

    render() {
        const { selectedLandingPage, isShowMessage, isSuccess, message, isInsert, isClickedSubmit, targetCountryList, isBlockAllTargetCountries, dealList } = this.state;
        const pageNameValidation = this._validatePageName();
        const pageCodeValidation = this._validatePageCode();
        const isValid = pageNameValidation.isValid && pageCodeValidation.isValid;
        const isShowPopup = this.props.selectedLandingPageId !== null;
        const title = isInsert ? "Add Landing Page" : "Edit Landing Page";

        return targetCountryList && dealList && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop="static" style={{ maxWidth: 550 }}>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="landing-page-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-2" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="pageName" sm={6} className="required pr-0">{headerText.LandingPageName}</Label>
                                    <Col sm={6}>
                                        <Input type="text" name="pageName" className="input-required mt-2" value={selectedLandingPage.pageName} onChange={this._handleChange}
                                            onKeyPress={(e) => this._handleKeyPress(e, isValid)} invalid={isClickedSubmit && !pageNameValidation.isValid} />
                                        {pageNameValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageNameValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="pageCode" sm={6} className="required">{headerText.LandingPageCode}</Label>
                                    <Col sm={6}>
                                        <Input type="text" name="pageCode" className="input-required mt-2" value={selectedLandingPage.pageCode} onChange={this._handleChange}
                                            onKeyPress={(e) => this._handleKeyPress(e, isValid)} invalid={isClickedSubmit && !pageCodeValidation.isValid} />
                                        {pageCodeValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageCodeValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="align-items-center mb-2">
                                    <Label sm={4}>Deal</Label>
                                    <Col sm={3}>
                                        <Input type="select" name="isExcluded" className='mt-1' value={Number(selectedLandingPage.isExcluded)} onChange={this._handleChangeFilterDealMode}>
                                            {
                                                Object.keys(filterDealMode).map(key => {
                                                    return (<option key={`isExcluded-${filterDealMode[key]}`} value={filterDealMode[key]}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                    <Col sm={5} >
                                        <div className='mt-1'>
                                            <MultipleSelect
                                                dataSource={dealList}
                                                keyField="key"
                                                valueField="name"
                                                statusField="checked"
                                                noneSelectedLabel="None"
                                                selectedAllLabel="All Deals"
                                                hasInputOption={true}
                                                onChange={this._handleChangeDeals} />
                                        </div>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="align-items-center mb-2">
                                    <Col sm={4}></Col>
                                    <Label sm={4} className="d-flex mt-1">
                                        <input type="checkbox" name="isMobile" checked={selectedLandingPage.isMobile} onChange={this._handleChange} />
                                        <span className='ml-2'>Is Mobile Only</span>
                                    </Label>
                                </FormGroup>
                                {
                                    targetCountryList && <BlockTargetCountryFormGroup
                                        sm={4}
                                        targetCountryList={targetCountryList}
                                        isBlockAllTargetCountries={isBlockAllTargetCountries}
                                        changeTargetCountry={this.handleChangeTargetCountry}
                                    />
                                }
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)} >Submit</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedLandingPageId, dealList }) {
    return Object.assign({}, { selectedLandingPageId, dealList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedLandingPageId, getLandingPageDetail, fetchData, insertLandingPage, updateLandingPage, toggleLoader, getTargetCountryListByLandingPage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateForm);