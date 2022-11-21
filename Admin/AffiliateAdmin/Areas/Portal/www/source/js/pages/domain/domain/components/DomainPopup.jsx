import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { defaultState, marketStatus } from '../libs/constants';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainById, getMarketListByDomain } from '../actions/index';

export class DomainPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState;
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId, isAffDomain } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId, isAffDomain);
        }
    }

    _handleUpdateState = async (selectedDomainId, isAffDomain) => {
        if (selectedDomainId === 0) {
            const marketListByDomains = await this.props.getMarketListByDomain({ affDomainId: 0, redirectDomainId: 0 });

            this.setState({ isInsert: true, selectedDomain: { domainId: selectedDomainId, domainName: '', blockMarkets: '', isSSL: false }, marketListByDomains });
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainById(selectedDomainId, isAffDomain);

            if (domain) {
                const { DomainName, IsSSL } = domain;
                var domainIdParam = isAffDomain ? "affDomainId" : "redirectDomainId";
                const marketListByDomains = await this.props.getMarketListByDomain({ [domainIdParam]: selectedDomainId });
                if (marketListByDomains) {
                    const usingMarkets = this._handleGetUsingMarkets(marketListByDomains);
                    const blockMarkets = this._handleGetBlockMarkets(marketListByDomains);
                    const isBlockAllMarkets = (usingMarkets.length + blockMarkets.length) === marketListByDomains.length;

                    this.setState({ isInsert: false, usingMarkets, selectedDomain: { domainId: selectedDomainId, domainName: DomainName, blockMarkets, isSSL: IsSSL }, marketListByDomains, isBlockAllMarkets });
                } else {
                    this.setState({ isInsert: false, selectedDomain: { domainId: selectedDomainId, domainName: DomainName, blockMarkets: '', isSSL: IsSSL } });
                }
            }
            this.props.toggleLoader(false);
        }
    }

    _handleGetBlockMarkets = (marketListByDomains) => {
        return marketListByDomains.filter(m => m.Status === marketStatus.Blocked).map(m => m.MarketId);
    }

    _handleGetUsingMarkets = (marketListByDomains) => {
        return marketListByDomains.filter(m => m.Status === marketStatus.Using).map(m => m.MarketId);
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        this.setState({ selectedDomain: newDomain });
    }

    _handleChangeMarket = (event, marketId) => {
        const isChecked = event.target.checked;

        const { selectedDomain, usingMarkets, marketListByDomains } = this.state;

        if (usingMarkets.includes(marketId)) {
            return;
        }

        const marketItem = marketListByDomains.find(item => item.MarketId === marketId && item.Status !== marketStatus.Using);
        if (marketItem) {
            const newMarketListByDomains = marketListByDomains.map(item => {
                if (item.MarketId !== marketId) {
                    return item;
                }
                return {
                    ...item,
                    Status: isChecked ? marketStatus.Blocked : marketStatus.Unblocked
                };
            });

            const blockMarkets = this._handleGetBlockMarkets(newMarketListByDomains);
            const newDomain = Object.assign({}, selectedDomain, { blockMarkets });
            const isBlockAllMarkets = (usingMarkets.length + blockMarkets.length) === newMarketListByDomains.length;

            this.setState({ selectedDomain: newDomain, marketListByDomains: newMarketListByDomains, isBlockAllMarkets });
        }
    }

    _handleChangeAllMarkets = (event) => {
        const isChecked = event.target.checked;

        const { selectedDomain, marketListByDomains } = this.state;

        console.log(marketListByDomains);

        if (marketListByDomains) {
            const status = isChecked ? marketStatus.Blocked : marketStatus.Unblocked;
            const newMarketListByDomains = marketListByDomains.map(item => {
                if (item.Status === marketStatus.Using) {
                    return item;
                }
                return {
                    ...item,
                    Status: status
                };
            });

            const blockMarkets = this._handleGetBlockMarkets(newMarketListByDomains);
            const newDomain = Object.assign({}, selectedDomain, { blockMarkets });

            console.log(newMarketListByDomains);

            this.setState({ selectedDomain: newDomain, marketListByDomains: newMarketListByDomains, isBlockAllMarkets: isChecked });
        }
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isAffDomain } = this.props;
        const { selectedDomain } = this.state;

        if(selectedDomain.domainName) {
            const { isInsert } = this.state;
            const domainInfo = Object.assign({}, selectedDomain, { isAffDomain, blockMarkets: selectedDomain.blockMarkets.join(',') })
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";

            const result = isInsert ? await this.props.insertDomain(domainInfo) : await this.props.updateDomain(domainInfo);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Domain is failed!" : "Update Domain is failed!");
            }

        } else {
            this._handleShowAlert(false, "Please input domain name!")
        }

        this.props.toggleLoader(false);
    }

    _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit();
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
            this.props.fetchData(this.props.isAffDomain);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, usingMarkets, marketListByDomains, isBlockAllMarkets } = this.state;
        const isShowPopup = selectedDomain !== null;
        const title = isInsert ? "Add Domain" : "Edit Domain";
        const buttonLabel = isInsert ? "Add" : "Submit";

        return selectedDomain && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop="static" style={{ maxWidth: 700 }}>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="domain-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-4" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="domainName" sm={3} className="required">Domain</Label>
                                    <Col sm={9}>
                                        <Input type="text" className="input-required mt-2" name="domainName"
                                            value={selectedDomain.domainName} onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="isSSL" sm={3}>SSL</Label>
                                    <Col sm={9}>
                                        <input type="checkbox" name="isSSL" id="isSSL" checked={selectedDomain.isSSL} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                                {
                                    marketListByDomains && (
                                        <FormGroup row className="mb-2">
                                            <Label sm={3}>Block Market</Label>
                                            <Col sm={9}>
                                                <div className="block-market">
                                                    {
                                                        marketListByDomains.map(item => {
                                                            const isUsing = usingMarkets.includes(item.MarketId);

                                                            return (
                                                                <div key={item.MarketId} className="block-market-item">
                                                                    <input type="checkbox" className="mr-2" disabled={isUsing} title={isUsing && "This Affiliate domain has been used for this market!"} id={item.MarketId} checked={item.Status === marketStatus.Blocked} onChange={(e) => this._handleChangeMarket(e, item.MarketId)} />
                                                                    <Label for={item.MarketId}>{item.MarketName}</Label>
                                                                </div>
                                                            )
                                                        })
                                                    }
                                                    <div className="block-market-item block-market-All">
                                                            <input type="checkbox" className="mr-2" id="All-Markets" checked={isBlockAllMarkets} onChange={this._handleChangeAllMarkets} />
                                                            <Label for="All-Markets">All</Label>
                                                        </div>
                                                </div>
                                            </Col>
                                        </FormGroup>
                                        )
                                }
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

function mapStateToProps({ selectedDomainId, isAffDomain }) {
    return Object.assign({}, { selectedDomainId, isAffDomain });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainById, getMarketListByDomain }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainPopup);