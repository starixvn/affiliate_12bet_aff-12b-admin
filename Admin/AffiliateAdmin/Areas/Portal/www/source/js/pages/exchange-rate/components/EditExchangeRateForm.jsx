import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { resource, message, regExp, money } from '../lib/constants';
import { updateExchangeRate, setTogglePopup, toggleLoader } from '../actions/index';
import { floatFormat, round } from '../../../modules/numberFormat';

const regexCheckRate = new RegExp(regExp.checkRate);
const regexValidRate = new RegExp(regExp.validRate);
const regexShouldFormatRate = new RegExp(regExp.shouldFormatRate);

export class EditChangeRateForm extends PureComponent {
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
        var { selectedCurrency } = this.props;
        if(selectedCurrency && prevProps.selectedCurrency != selectedCurrency) {
            this.setState({ rate: floatFormat(selectedCurrency.Rate, 5) });
        }
    }

    _handleChangeRate = (event) => {
        const value = event.target.value;
        const name = event.target.name;
        const rate = value.replace(/,/g, '');

        if (regexCheckRate.test(rate)) {
            if (regexShouldFormatRate.test(rate) && Number(rate) !== 0 && Number(rate) > money.MinValue && Number(rate) < money.MaxValue) {
                this.setState({ [name]: floatFormat(Number(rate), 5) })
            } else {
                this.setState({ [name]: value });
            }
        }
    }

    _isValidRate = (rate) => {
        return regexValidRate.test(rate) && Number(rate) > money.MinValue && Number(rate) < money.MaxValue;
    }

    _validateRate = () => {
        const { rate } = this.state;
        const result = {
            isValid: this._isValidRate(rate) && Number(rate) != 0,
            message: ''
        }

        if (!result.isValid) {
            result.message = rate === '' ? message.Required : message.Invalid;
        }

        return result;
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        if (rateValidation.isValid) {
            const { affInfo } = this.props;
            const { rate } = this.state;
            const rateNumber = Number(rate.replace(/,/g, ''));
            const param = Object.assign({}, this.props.selectedCurrency, { Rate: rateNumber });
            const result = await this.props.updateExchangeRate(param);

            this.setState({
                isShowMessage: true,
                isSuccess: result.IsSuccessful,
                message: result.IsSuccessful ? message.Success : result.Message,
                rate: result.IsSuccessful ? '' : rate
            }, () => window.setTimeout(this.hideMessage, 3000));

            if (result.IsSuccessful) {
                window.setTimeout(() => this.props.fetchData(this.props.param), 3000);
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: rateValidation.message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit();
        }
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { selectedCurrency, isOpenPopup } = this.props;
        const { rate, isSuccess, isShowMessage, message } = this.state;
        const isValidRate = this._isValidRate(rate) || rate === '';

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
                                    <Label sm={8}>{selectedCurrency.Currency}</Label>
                                </FormGroup>

                                <FormGroup row className={isValidRate ? "mb-2" : "mb-2 has-error"}>
                                    <Label for="rate" sm={4} className="required">{selectedCurrency.isActualRate ? resource.US$Rate : resource.Buying}</Label>
                                    <Col sm={8}>
                                        <Input type="text" className="input-required mt-2" name="rate" id="rate" autocomplete="false"
                                            value={rate} onChange={this._handleChangeRate} onKeyPress={this._handleKeyPress}/>
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

function mapStateToProps({ param, selectedCurrency, isOpenPopup }) {
    return Object.assign({}, { param, selectedCurrency, isOpenPopup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateExchangeRate, setTogglePopup, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditChangeRateForm);