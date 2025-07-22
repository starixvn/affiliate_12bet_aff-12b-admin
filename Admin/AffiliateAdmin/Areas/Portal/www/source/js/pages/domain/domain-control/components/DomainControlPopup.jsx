import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';

import Select from '../../../../components/select';
import MultipleSelect from '../../../../components/multiple-select';

import { insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getTargetCountryListByDomains } from '../actions/index';
import { defaultState, filterDealMode } from '../libs/constants';
import { handleChangeMultipleSelected } from '../../../../modules/utils';

export class DomainControlPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState;
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId);
        }
    }

    _handleUpdateState = async (selectedDomainId) => {
        if (selectedDomainId === 0) {
            const { affDomainList, redirectDomainList, dealList } = this.props;

            if ((affDomainList && affDomainList.length) && (redirectDomainList && redirectDomainList.length)) {
                const affDomainId = affDomainList[0].DomainId;
                const redirectDomainId = redirectDomainList[0].DomainId;

                const targetCountryListByDomains = await this.props.getTargetCountryListByDomains({ affDomainId, redirectDomainId });
                const targetCountryCode = targetCountryListByDomains ? targetCountryListByDomains[0].TargetCountryCode : "";

                this.setState({
                    isInsert: true,
                    selectedDomain: {
                        domainControlId: selectedDomainId,
                        affDomainId,
                        redirectDomainId,
                        targetCountryCode,
                        isUsed: true,
                        isExcluded: filterDealMode.Exclude,
                        dealIds: '',
                        isMobile: false
                    },
                    targetCountryListByDomains,
                    dealList
                });
            }
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainControlById(selectedDomainId);

            if (domain) {
                const { AffDomainId, RedirectDomainId, TargetCountryCode, IsUsed, IsExcluded, DealIds, IsMobile } = domain;
                const domainIds = { affDomainId: AffDomainId, redirectDomainId: RedirectDomainId };
                const targetCountryListByDomains = await this.props.getTargetCountryListByDomains(domainIds);
                const targetCountryCode = this._handleGetAvailableCountryCode(TargetCountryCode, targetCountryListByDomains);
                const newDealList = handleChangeMultipleSelected(this.props.dealList, DealIds);

                this.setState({
                    isInsert: false,
                    selectedDomain: {
                        domainControlId: selectedDomainId,
                        ...domainIds,
                        targetCountryCode,
                        isUsed: IsUsed,
                        isExcluded: IsExcluded,
                        dealIds: DealIds,
                        isMobile: IsMobile
                    },
                    targetCountryListByDomains,
                    dealList: newDealList
                });
            } else {
                this.props.setSelectedDomainId(null);
            }
            this.props.toggleLoader(false);
        }
    }

    _handleGetAvailableCountryCode = (targetCountryCode, targetCountryList) => {
        if (targetCountryList && targetCountryList.length && !targetCountryList.some(t => t.TargetCountryCode == targetCountryCode)) {
            return targetCountryList[0].TargetCountryCode;
        }

        return targetCountryCode;
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedDomain } = this.state;

        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        this.setState({ selectedDomain: newDomain });
    }

    _handleChangeAffDomain = (selectedDomainId) => {
        this._handleChangeDomain("affDomainId", selectedDomainId);
    }

    _handleChangeRedirectDomain = (selectedDomainId) => {
        this._handleChangeDomain("redirectDomainId", selectedDomainId);
    }

    _handleChangeDomain = async (domainType, domainId) => {
        this.props.toggleLoader(true);

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [domainType]: domainId });

        const { affDomainId, redirectDomainId } = newDomain;

        const targetCountryListByDomains = await this.props.getTargetCountryListByDomains({ affDomainId, redirectDomainId });
        const selectedTargetCountryByDomains = targetCountryListByDomains ? targetCountryListByDomains[0].TargetCountryCode : "";

        this.setState({ selectedDomain: { ...newDomain, targetCountryCode: selectedTargetCountryByDomains }, targetCountryListByDomains });
        this.props.toggleLoader(false);
    }

    _handleChangeFilterDealMode = (event) => {
        const { value } = event.target;
        this.setState({
            selectedDomain: {
                ...this.state.selectedDomain,
                isExcluded: Number(value),
                dealIds: ''
            },
            dealList: handleChangeMultipleSelected(this.state.dealList, '')
        });
    }

    _handleChangeDeals = (item, selectedDeals) => {
        const isAllDeals = selectedDeals.split(',').length === this.props.dealList.length;
        this.setState({
            selectedDomain: { ...this.state.selectedDomain, dealIds: isAllDeals ? '0' : selectedDeals },
            dealList: handleChangeMultipleSelected(this.state.dealList, selectedDeals)
        });
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isInsert, selectedDomain } = this.state;

        const validateMessage = this._validateSelectedDealsByMode(selectedDomain.isExcluded, selectedDomain.dealIds);
        if (validateMessage) {
            this._handleShowAlert(false, validateMessage);
        } else {
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";

            const result = isInsert ? await this.props.insertDomain(selectedDomain) : await this.props.updateDomain(selectedDomain);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Domain is failed!" : "Update Domain is failed!");
            }
        }
        this.props.toggleLoader(false);
    }

    _validateSelectedDealsByMode = (isExcluded, selectedDeals) => {
        if (isExcluded === filterDealMode.Exclude && selectedDeals === '0') {
            return 'Selecting all options is not allowed. Please unselect at least one.';
        }

        if (isExcluded === filterDealMode.Include && selectedDeals === '') {
            return 'Please select at least one deal.';
        }

        return '';
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
            this.props.fetchData(this.props.selectedTargetCountry, this.props.affSubDomainList, true);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { selectedDomainId, affDomainList, redirectDomainList } = this.props;
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, targetCountryListByDomains, dealList } = this.state;
        const isShowPopup = selectedDomainId !== null;
        const isDisabled = targetCountryListByDomains == null;
        const title = isInsert ? "Add New Domain Control" : "Edit Domain Control";
        const buttonLabel = isInsert ? "Add" : "Submit";

        return selectedDomain && affDomainList && redirectDomainList && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="domain-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-4" autoComplete="off">
                                <FormGroup row className="align-items-center mb-2">
                                    <Label htmlFor="affDomainId" sm={4}>Aff Domain</Label>
                                    <Col sm={8}>
                                        <Select
                                            id="affDomain"
                                            dataSource={affDomainList}
                                            keyField="DomainId"
                                            valueField="DomainName"
                                            hasInputOption={true}
                                            selectedKey={selectedDomain.affDomainId}
                                            onChange={this._handleChangeAffDomain} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="align-items-center mb-2">
                                    <Label htmlFor="redirectDomainId" sm={4}>Redirect Domain</Label>
                                    <Col sm={8}>
                                        <Select
                                            id="redirectDomain"
                                            dataSource={redirectDomainList}
                                            keyField="DomainId"
                                            valueField="DomainName"
                                            hasInputOption={true}
                                            selectedKey={selectedDomain.redirectDomainId}
                                            onChange={this._handleChangeRedirectDomain} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="align-items-center mb-2">
                                    <Label htmlFor="targetCountryCode" sm={4}>Target Country</Label>
                                    <Col sm={8}>
                                        {
                                            isDisabled ? (
                                                <Input type="select" name="targetCountryCode" disabled="true" title="All target countries are blocked for these domains!" className="mt-1" value="">
                                                    <option key="targetCountryCode-none" value="">Please select!</option>
                                                </Input>
                                            ) : (
                                                <Input type="select" name="targetCountryCode" className="mt-1" value={selectedDomain.targetCountryCode} onChange={this._handleChange}>
                                                    {
                                                        targetCountryListByDomains.map(item => {
                                                            return (<option key={`targetCountryCode-${item.TargetCountryCode}`} value={item.TargetCountryCode}>{item.TargetCountryName}</option>)
                                                        })
                                                    }
                                                </Input>
                                            )
                                        }
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="align-items-center mb-2">
                                    <Label sm={4}>Deal</Label>
                                    <Col sm={3}>
                                        <Input type="select" name="isExcluded" className='mt-1' value={Number(selectedDomain.isExcluded)} onChange={this._handleChangeFilterDealMode}>
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
                                    <Label sm={3} className="d-flex mt-1">
                                        <input type="checkbox" name="isUsed" checked={selectedDomain.isUsed} onChange={this._handleChange} />
                                        <span className='ml-2'>Is Active</span>
                                    </Label>
                                    <Label sm={4} className="d-flex mt-1">
                                        <input type="checkbox" name="isMobile" checked={selectedDomain.isMobile} onChange={this._handleChange} />
                                        <span className='ml-2'>Is Mobile Only</span>
                                    </Label>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>Cancel</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedDomainId, selectedTargetCountry, targetCountryList, affDomainList, redirectDomainList, affSubDomainList, dealList }) {
    return Object.assign({}, { selectedDomainId, selectedTargetCountry, targetCountryList, affDomainList, redirectDomainList, affSubDomainList, dealList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getTargetCountryListByDomains }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainControlPopup);