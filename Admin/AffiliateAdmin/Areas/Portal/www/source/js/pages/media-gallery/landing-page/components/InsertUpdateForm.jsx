import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import BlockedTargetCountryFormGroup from '../../../common/components/BlockedTargetCountryFormGroup';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert, FormFeedback } from 'reactstrap';
import { setSelectedLandingPageId, getLandingPageDetail, fetchData, insertLandingPage, updateLandingPage, toggleLoader, getTargetCountryList, getTargetCountryListByLandingPage } from '../actions/index';
import { defaultState, headerText, globalResource, messages, restrictedCharactersPattern, maxLength } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { getBlockedTargetCountryList, checkBlockedAllTargetCountries } from '../../../common/modules/utils/targetcountry';
import { htmlDecode } from '../../../../modules/htmlHelper';

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
            const visibleTargetCountryList = await this.props.getTargetCountryList();

            this.setState({ isInsert: true, selectedLandingPage: { pageName: '', pageCode: '', blockTargetCountry: '' }, targetCountryList: visibleTargetCountryList });
        } else {
            const landingPage = await this.props.getLandingPageDetail(pageId);

            if (landingPage) {
                const pageName = htmlDecode(landingPage.PageName);
                const pageCode = htmlDecode(landingPage.PageCode);

                const targetCountryList = await this.props.getTargetCountryListByLandingPage({ pageId });

                if (targetCountryList && targetCountryList.length) {
                    const blockedTargetCountryList = getBlockedTargetCountryList(targetCountryList);
                    const isBlockedAllTargetCountries = checkBlockedAllTargetCountries(targetCountryList, blockedTargetCountryList);

                    this.setState({ isInsert: false, selectedLandingPage: { pageName, pageCode, blockTargetCountry: blockedTargetCountryList.join(',') }, targetCountryList, isBlockedAllTargetCountries });
                } else {
                    this.setState({ isInsert: false, selectedLandingPage: { pageName, pageCode, blockTargetCountry: '' } });
                }
            }
        }

        this.props.toggleLoader(false);
    }

    _handleChange = (event) => {
        const { name, value } = event.target;

        const { selectedLandingPage } = this.state;
        const newPage = Object.assign({}, selectedLandingPage, { [name]: value });

        this.setState({ selectedLandingPage: newPage });
    }

    handleChangeTargetCountry = (isBlockedAllTargetCountries, blockTargetCountry, newTargetCountryList) => {
        const { selectedLandingPage } = this.state;
        const newPage = Object.assign({}, selectedLandingPage, { blockTargetCountry });

        this.setState({ selectedLandingPage: newPage, targetCountryList: newTargetCountryList, isBlockedAllTargetCountries });
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

            const result = isInsert ? await this.props.insertLandingPage(landingPageInfo) : await this.props.updateLandingPage(landingPageInfo);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
            }
        }

        this.props.toggleLoader(false);
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
        const { selectedLandingPage, isShowMessage, isSuccess, message, isInsert, isClickedSubmit, targetCountryList, isBlockedAllTargetCountries } = this.state;
        const pageNameValidation = this._validatePageName();
        const pageCodeValidation = this._validatePageCode();
        const isValid = pageNameValidation.isValid && pageCodeValidation.isValid;
        const isShowPopup = this.props.selectedLandingPageId !== null;
        const title = isInsert ? "Add Landing Page" : "Edit Landing Page";

        return (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop="static" style={{ maxWidth: 810 }}>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="landing-page-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-3" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="pageName" sm={4} className="required pr-0">{headerText.LandingPageName}</Label>
                                    <Col sm={5}>
                                        <Input type="text" name="pageName" className="input-required mt-2" value={selectedLandingPage.pageName} onChange={this._handleChange}
                                            onKeyPress={(e) => this._handleKeyPress(e, isValid)} invalid={isClickedSubmit && !pageNameValidation.isValid} />
                                        {pageNameValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageNameValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="pageCode" sm={4} className="required">{headerText.LandingPageCode}</Label>
                                    <Col sm={5}>
                                        <Input type="text" name="pageCode" className="input-required mt-2" value={selectedLandingPage.pageCode} onChange={this._handleChange}
                                            onKeyPress={(e) => this._handleKeyPress(e, isValid)} invalid={isClickedSubmit && !pageCodeValidation.isValid} />
                                        {pageCodeValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageCodeValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <BlockedTargetCountryFormGroup
                                    sm={4}
                                    targetCountryList={targetCountryList}
                                    isBlockedAllTargetCountries={isBlockedAllTargetCountries}
                                    changeTargetCountry={this.handleChangeTargetCountry}
                                />
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

function mapStateToProps({ selectedLandingPageId }) {
    return Object.assign({}, { selectedLandingPageId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedLandingPageId, getLandingPageDetail, fetchData, insertLandingPage, updateLandingPage, toggleLoader, getTargetCountryList, getTargetCountryListByLandingPage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateForm);