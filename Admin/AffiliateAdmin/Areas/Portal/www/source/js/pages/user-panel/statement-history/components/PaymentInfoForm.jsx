import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody } from 'reactstrap';

import { setTransactionId } from '../actions';
import { resolveClientUrl } from '../../../../modules/utils';

export class PaymentInfoForm extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        window.addEventListener('message', this.handleClosePopupEvent);
    }

    componentWillUnmount() {
        window.removeEventListener('message', this.handleClosePopupEvent);
    }

    handleClosePopupEvent = (event) => {
        if (event.data === 'closePopup') {
            this.props.setTransactionId(null);
        }
    }

    render() {
        const { transactionId } = this.props;
        const url = transactionId ? resolveClientUrl(`Portal/PaymentRequest/GetPaymentRequestDetail?transid=${transactionId}`) : '';
        return (
            <div className="data-content">
                <Modal isOpen={transactionId} toggle={() => this.props.setTransactionId(null)} size="lg" backdrop='static'>
                    <ModalHeader toggle={() => this.props.setTransactionId(null)}><span className="iconpopup"></span>Trxn.ID {transactionId}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper embed-responsive embed-responsive-4by3">
                            <iframe className="embed-responsive-item" src={url}></iframe>
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
    return bindActionCreators({ setTransactionId }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PaymentInfoForm);