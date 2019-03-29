import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Button, Form } from 'reactstrap';

import { toggleLoader, handleUpdate } from '../../actions';
import { resource, globalResource } from '../../modules/constants';
import { PaymentRequestDetail } from '../../shared/PaymentRequestDetail';

export class PaymentRequestDetailForm extends Component {
    constructor(props) {
        super(props);

        const { paymentRequestInfo } = this.props;

        this.state = {
            status: null,
            adminDesc: paymentRequestInfo.AdminDesc,
            affDesc: paymentRequestInfo.AffDesc,
            isSuccess: true,
            isShowMessage: false,
            message: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleSave = async () => {
        this.setState({
            isShowMessage: true
        });

        const { paymentRequestInfo } = this.props;

        window.scrollTo({ top: 0, behavior: "smooth" });
        if (this.state.status && paymentRequestInfo.Status.toLowerCase() == 'pending') {
            const { status, adminDesc, affDesc } = this.state;
            const paymentInfo = Object.assign({}, {
                TransId: paymentRequestInfo.TransId, AffId: paymentRequestInfo.AffId, Amount: paymentRequestInfo.Amount,
                TransDate: paymentRequestInfo.TransDateText, Status: status, AdminDesc: adminDesc, AffDesc: affDesc
            });
            const data = await this.props.handleUpdate(paymentInfo);

            if (data) {
                this.setState({
                    isSuccess: data.IsSuccessful,
                    message: data.IsSuccessful ? `Transaction ID ${paymentRequestInfo.TransId} has been updated successfully!` : data.Message,
                }, () => window.setTimeout(this.hideMessage, 3000));

                if (data.IsSuccessful) {
                    parent.postMessage('fetchData', window.location.origin);
                    window.setTimeout(() => this.handleClosePopup(), 3000);
                }
            }
            else {
                this.setState({
                    isSuccess: false,
                    message: `Transaction ID ${paymentRequestInfo.TransId} has been updated failly!`,
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        } else {
            this.setState({
                isSuccess: false,
                message: resource.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    handleClosePopup = () => {
        parent.postMessage('closePopup', window.location.origin);
    }

    render() {
        const { paymentRequestInfo } = this.props;
        const { adminDesc, affDesc, isSuccess, isShowMessage, message } = this.state;
        const isPending = paymentRequestInfo && paymentRequestInfo.Status && paymentRequestInfo.Status.toLowerCase() == 'pending';

        return paymentRequestInfo && (
            <Form tag="form" className="payment-request-form">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <PaymentRequestDetail
                    paymentRequestInfo={paymentRequestInfo}
                    adminDesc={adminDesc}
                    affDesc={affDesc}
                    isPending={isPending}
                    handleChange={this.handleChange}
                    handleChecked={this.handleChecked}
                />
                <Button type="button" color="success" className="mr-3" disabled={!isPending} onClick={this.handleSave}>{globalResource.Update}</Button>
                <Button type="button" color="info" onClick={this.handleClosePopup}>{globalResource.Cancel}</Button>
            </Form>
        );
    }
}

function mapStateToProps({ paymentRequestInfo }) {
    return Object.assign({}, { paymentRequestInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, handleUpdate }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PaymentRequestDetailForm);