import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { resource } from '../lib/constants';
import { updateExchangeRate, setTogglePopup } from '../actions/index';

export class EditChangeRateForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            rate: 0,
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        var { selectedCurrency } = this.props;
        if(selectedCurrency && prevProps.selectedCurrency != selectedCurrency) {
            this.setState({ rate: selectedCurrency.Rate });
        }
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _handleSubmit = () => {
        const { rate } = this.state;
        if(!rate) {
            this._handleAlertError('Exchange rate is required.');
        }

        const param = Object.assign({}, this.props.selectedCurrency, { Rate: this.state.rate });
    }

    _handleKeyPress = (event) => {
        var charCode = event.charCode;
        if (charCode === 13) {
            event.preventDefault();
            this.handleSubmit();
        }
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46)) {
            event.preventDefault();
            this._handleAlertError('Please input digit only.');
        }
    }

    _handleAlertError = (message) => {
        this.setState({
            isShowMessage: true,
            isSuccess: false,
            message
        }, () => window.setTimeout(this.hideMessage, 2000));
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { selectedCurrency, isOpenPopup } = this.props;
        const { rate, isSuccess, isShowMessage, message } = this.state;

        return selectedCurrency && (
            <Modal isOpen={isOpenPopup} toggle={() => this.props.setTogglePopup(false)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setTogglePopup(false)}><span className="iconpopup"></span>{resource.Title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="exchange-rate-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{resource.Currency}</Label>
                                    <Col sm={8}>
                                        <Label sm={4}>{selectedCurrency.Currency}</Label>
                                    </Col>
                                </FormGroup>

                                <FormGroup row className="mb-2">
                                    <Label for="rate" sm={4} className="required">{selectedCurrency.isActualRate ? resource.US$Rate : resource.Buying}</Label>
                                    <Col sm={8}>
                                        <Input type="text" className="input-required" name="rate" id="rate" autocomplete="false"
                                            value={rate} onChange={this._handleChange} onKeyPress={this._handleKeyPress}/>
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={() => this._handleSubmit}>{resource.Submit}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setTogglePopup(false)}>{resource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedCurrency, isOpenPopup }) {
    return Object.assign({}, { selectedCurrency, isOpenPopup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateExchangeRate, setTogglePopup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditChangeRateForm);