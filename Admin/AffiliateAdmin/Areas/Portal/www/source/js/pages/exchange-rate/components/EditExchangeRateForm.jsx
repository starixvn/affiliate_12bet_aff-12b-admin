import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { resource, message, regExp, money } from '../lib/constants';
import { updateExchangeRate, setTogglePopup, toggleLoader, fetchData } from '../actions/index';
import { format } from '../../../modules/numberFormat';

const regexCheckRate = new RegExp(regExp.checkRate);
const regexValidRate = new RegExp(regExp.validRate);
const regexShouldFormatRate = new RegExp(regExp.shouldFormatRate);

export class EditExchangeRateForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            rate: '',
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        var { selectedExchangeRate } = this.props;
        if (selectedExchangeRate && prevProps.selectedExchangeRate != selectedExchangeRate) {
            this.setState({ rate: selectedExchangeRate.Rate });
        }
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;
        const rate = value.replace(/,/g, '');

        if (regexCheckRate.test(rate)) {
            if (regexShouldFormatRate.test(rate) && Number(rate) > 0 && Number(rate) < money.MaxValue) {
                this.setState({ [name]: format(Number(rate)) })
            }
            else {
                if (Number(rate) >= money.MaxValue || value.length > 25) {
                    this._handleShowAlert(false, message.ExceedLimit);
                    return;
                }

                this.setState({ [name]: value });
            }
        }
    }

    _isValidRate = (rate) => {
        return regexValidRate.test(rate) && Number(rate) < money.MaxValue;
    }

    _validateRate = () => {
        const rate = this.state.rate.replace(/,/g, '');
        const result = {
            isValid: this._isValidRate(rate) && Number(rate) > 0,
            message: ''
        }

        if (!result.isValid) {
            result.message = rate === '' ? message.Required : message.Invalid;
        }

        return result;
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const rateValidation = this._validateRate();

        if (rateValidation.isValid) {
            const { rate } = this.state;
            const rateNumber = Number(rate.replace(/,/g, ''));
            const newExchangeRate = Object.assign({}, this.props.selectedExchangeRate, { Rate: rateNumber });
            const result = await this.props.updateExchangeRate(newExchangeRate);

            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message.Success : result.Message);

            if (result.IsSuccessful) {
                window.setTimeout(() => {
                    this.props.setTogglePopup(false);
                    this.props.fetchData(this.props.date);
                }, 3000);
            }
        } else {
            this._handleShowAlert(false, rateValidation.message);
        }

        this.props.toggleLoader(false);
    }

    _handleKeyPress = (event) => {
        const charCode = event.charCode;
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit();
        }
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46)) {
            event.preventDefault();
            this._handleShowAlert(false, message.InputDigit);
        }
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(this._hideMessage, 3000));
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { selectedExchangeRate, isOpenPopup } = this.props;
        const { rate, isSuccess, isShowMessage, message } = this.state;
        const isValidRate = this._isValidRate(rate.replace(/,/g, '')) || rate === '';

        return selectedExchangeRate && (
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
                                    <Label sm={8}>{selectedExchangeRate.Currency}</Label>
                                </FormGroup>

                                <FormGroup row className={isValidRate ? "mb-2" : "mb-2 has-error"}>
                                    <Label for="rate" sm={4} className="required">{selectedExchangeRate.RateType ? resource.Buying : resource.US$Rate}</Label>
                                    <Col sm={8}>
                                        <Input type="text" className="input-required mt-2" name="rate" id="rate" autocomplete="false"
                                            value={rate} onChange={this._handleChange} onKeyPress={this._handleKeyPress} maxLength="26" />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>{resource.Submit}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setTogglePopup(false)}>{resource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ date, selectedExchangeRate, isOpenPopup }) {
    return Object.assign({}, { date, selectedExchangeRate, isOpenPopup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateExchangeRate, setTogglePopup, toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditExchangeRateForm);