import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody, Alert, Button, Form } from 'reactstrap';

import { setTransactionId, toggleLoader, getPaymentRequestInfo } from '../actions';
import { resource, globalResource } from '../modules/constants';
import { PaymentRequestInfo } from '../shared/PaymentRequestInfo';

export class PaymentRequestUpdate extends Component {
    constructor(props) {
        super(props);

        this.state = {
            paymentRequestInfo: null,
            status: '',
            adminDesc: '',
            affDesc: '',
            isSuccess: true,
            isShowMessage: false,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { transactionId } = this.props;

        if (transactionId !== prevProps.transactionId) {
            this.handleGetPaymentRequestInfo(transactionId);
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }
    
    handleGetPaymentRequestInfo = async(transId) => {
        this.props.toggleLoader(true);

        const data = await this.props.getPaymentRequestInfo({transId});

        data && this.setState({ paymentRequestInfo: data, status: data.status, adminDesc: data.AdminDesc, affDesc: data.AffDesc });

        this.props.toggleLoader(false);
    }

    handleSave = async() => {
        this.props.toggleLoader(true);
        this.setState({
            isShowMessage: true
        });

        const { paymentRequestInfo } = this.state;

        if (paymentRequestInfo.Status.toLowerCase() == 'pending' && this.state.status != '') {
            const { status, adminDesc, affDesc } = this.state;
            const paymentInfo = Object.assign({}, paymentRequestInfo ,{ Status: status, AdminDesc: adminDesc, AffDesc: affDesc });
            const data = await this.props.handleUpdate(paymentInfo);

            this.setState({
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? "Update is successful!" : data.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));

        } else {
            this.setState({
                isSuccess: false,
                message: resource.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { transactionId } = this.props;
        const { paymentRequestInfo, adminDesc, affDesc, isSuccess, isShowMessage, message } = this.state;
        const isPending = paymentRequestInfo && paymentRequestInfo.Status.toLowerCase() == 'pending';

        return  paymentRequestInfo && (
            <div className="data-content">
                <Modal isOpen={transactionId} toggle={() => this.props.setTransactionId(null)} size="lg" backdrop='static'>
                    <ModalHeader toggle={() => this.props.setTransactionId(null)}><span className="iconpopup"></span>Trxn.ID {transactionId}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <Form tag="form" className="payment-request-form">
                                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                                </Alert>
                                <PaymentRequestInfo
                                    paymentRequestInfo={paymentRequestInfo}
                                    adminDesc={adminDesc}
                                    affDesc={affDesc}
                                    handleChange={this.handleChange}
                                    handleChecked={this.handleChecked}   
                                />
                                <Button type="button" color="success" className="mr-3" disabled={!isPending} onClick={this.handleSave}>{globalResource.Update}</Button>
                                <Button type="button" color="info" onClick={this.props.setTransactionId(null)}>{globalResource.Cancel}</Button>
                            </Form>
                        </div>
                    </ModalBody>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps({ transactionId }) {
    return Object.assign({}, { transactionId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setTransactionId, toggleLoader, getPaymentRequestInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PaymentRequestUpdate);