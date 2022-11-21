import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { headerText, defaultState } from '../libs/constants';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getMarketListByDomains } from '../actions/index';

export class DomainControlPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId);
        }
    }

    _handleUpdateState = async (selectedDomainId) => {
        if (selectedDomainId === 0) {
            const { affDomainList, redirectDomainList } = this.props;

            if ((affDomainList && affDomainList.length) && (redirectDomainList && redirectDomainList.length)) {
                const affDomainId = affDomainList[0].DomainId;
                const redirectDomainId = redirectDomainList[0].DomainId;

                const marketListByDomains = await this.props.getMarketListByDomains({ affDomainId, redirectDomainId });
                const marketId = marketListByDomains ? marketListByDomains[0].MarketId : "";

                this.setState({ isInsert: true, selectedDomain: { domainControlId: selectedDomainId, affDomainId, redirectDomainId, marketId, status: false }, marketListByDomains });
            }
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainControlById(selectedDomainId);

            if (domain) {
                const { AffDomainId, RedirectDomainId, MarketId, Status } = domain;
                const domainIds = { affDomainId: AffDomainId, redirectDomainId: RedirectDomainId };
                const marketListByDomains = await this.props.getMarketListByDomains(domainIds);

                this.setState({ isInsert: false, selectedDomain: { domainControlId: selectedDomainId, ...domainIds, marketId: MarketId, status: Status }, marketListByDomains });
            }
            this.props.toggleLoader(false);
        }
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        this.setState({ selectedDomain: newDomain });
    }

    _handleChangeDomain = async (event) => {
        this.props.toggleLoader(true);

        const target = event.target;
        const value = target.value;

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        const { affDomainId, redirectDomainId } = newDomain;

        const marketListByDomains = await this.props.getMarketListByDomains({ affDomainId, redirectDomainId });
        const selectedMarketIdByDomains = marketListByDomains && marketListByDomains[0]?.MarketId || "";

        this.setState({ selectedDomain: { ...newDomain, marketId: selectedMarketIdByDomains }, marketListByDomains });
        this.props.toggleLoader(false);
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isInsert, selectedDomain } = this.state;
        const message = isInsert ? "Add Successfully!" : "Update Successfully!";

        const result = isInsert ? await this.props.insertDomain(selectedDomain) : await this.props.updateDomain(selectedDomain);

        if (result) {
            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            this._handleShowAlert(false, isInsert ? "Add Domain is failed!" : "Update Domain is failed!");
        }

        this.props.toggleLoader(false);
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
            this.props.fetchData(this.props.selectedMarketId);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { selectedDomainId, affDomainList, redirectDomainList } = this.props;
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, marketListByDomains } = this.state;
        const isShowPopup = selectedDomainId !== null;
        const isDisabled = marketListByDomains == null;
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
                            <Form tag="form" className="mb-5" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="affDomainId" sm={4}>{headerText.AffDomain}</Label>
                                    <Col sm={8}>
                                        <Input type="select" name="affDomainId" className="mt-1" value={selectedDomain.affDomainId} onChange={this._handleChangeDomain}>
                                            {
                                                affDomainList.map(item => {
                                                    return (<option key={`affDomain-${item.DomainId}`} value={item.DomainId}>{item.DomainName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="redirectDomainId" sm={4}>{headerText.RedirectDomain}</Label>
                                    <Col sm={8}>
                                        <Input type="select" name="redirectDomainId" className="mt-1" value={selectedDomain.redirectDomainId} onChange={this._handleChangeDomain}>
                                            {
                                                redirectDomainList.map(item => {
                                                    return (<option key={`redirectDomain-${item.DomainId}`} value={item.DomainId}>{item.DomainName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="marketId" sm={4}>{headerText.Market}</Label>
                                    <Col sm={8}>
                                        {
                                            isDisabled ? (
                                                <Input type="select" name="marketId" disabled="true" title="All markets are blocked for these domains!" className="mt-1" value="">
                                                    <option key="market-none" value="">Please select!</option>
                                                </Input>
                                            ) : (
                                                    <Input type="select" name="marketId" className="mt-1" value={selectedDomain.marketId} onChange={this._handleChange}>
                                                        {
                                                            marketListByDomains.map(item => {
                                                                return (<option key={`market-${item.MarketId}`} value={item.MarketId}>{item.MarketName}</option>)
                                                            })
                                                        }
                                                    </Input>
                                                )
                                        }
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{headerText.Status}</Label>
                                    <Col sm={8}>
                                        <input type="checkbox" name="status" checked={selectedDomain.status} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={isDisabled || !selectedDomain.marketId} onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>Cancel</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedDomainId, selectedMarketId, marketList, affDomainList, redirectDomainList }) {
    return Object.assign({}, { selectedDomainId, selectedMarketId, marketList, affDomainList, redirectDomainList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getMarketListByDomains }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainControlPopup);