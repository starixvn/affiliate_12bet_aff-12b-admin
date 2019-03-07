import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Alert, Col, Button, Form, FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { toggleLoader } from '../../../../actions/loaderAction';
import { post } from '../../../../modules/ajaxRequest';

import { resource } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            amount: '',
            description: '',
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidAmount() {
        const { affId } = this.props;
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _validateAmount = () => {
        const { amount } = this.state;

        const result = {
            isValid: true,
            message: ''
        }

        if (!result.isValid) {
            result.message = "The amount is invalid";
        }

        return result;
    }

    handleSubmit = () => {
        const adjustmentInfo = Object.assign({}, { AffId: this.props.affId, Amount: this.state.amount, Description: this.state.description });
    }

    render() {
        const { isShowMessage, isSuccess, message } = this.state;
        const amountValidation = this._validateAmount();

        return (
            <div className="Adjustment-form-container">
                <div class="aff-name">12BETAFF226</div>
                <div class="clear"></div>
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <Form tag="form" className="mb-2" autocomplete="off">
                    <FormGroup row className="mb-2">
                        <Label sm={3}>Balance Brought Forward</Label>
                        <Label sm={9}>$9,787,197.87</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label for="amount" sm={3}>Amount</Label>
                        <Col sm={9}>
                            <Input type="text" name="amount" id="amount"
                                value={this.state.amount} onChange={this.handleChange} invalid={!amountValidation.isValid} />
                            {amountValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: amountValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label for="description" sm={3} className="d-flex align-items-center required">Description</Label>
                        <Col sm={9}>
                            <Input type="textarea" name="description" id="description" rows="10"
                                value={this.state.description} onChange={this.handleChange} invalid={!amountValidation.isValid} />
                            {amountValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: amountValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label sm={3}></Label>
                        <Col sm={9}>
                            <Button type="button" color="success" disabled={!amountValidation.isValid} onClick={this.handleSubmit}>Submit</Button>
                        </Col>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ affId }) {
    return Object.assign({}, { affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);