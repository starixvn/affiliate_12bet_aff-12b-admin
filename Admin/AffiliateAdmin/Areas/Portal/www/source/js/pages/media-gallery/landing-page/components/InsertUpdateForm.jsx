import React, { Component } from 'react';
import { Col, Button, Form, FormGroup, Label, Input, FormFeedback } from 'reactstrap';

const requiredErrorMessage = 'This field is required';
const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;
const maxLength = 50;
const maxLengthErrorMessage = 'This field must not exceed 50 characters';

export default class InsertUpdateForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: this.props.data
        }
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        const data = Object.assign({}, this.state.data, { [name]: value });

        this.setState({ data });
    }

    handleSave = () => {
        const { data } = this.state;

        if (data.PageName && this._validatePageCode().isValid) {
            this.props.onSave(data);
        }
    }

    _validatePageCode() {
        const { PageCode } = this.state.data;
        const hasRestrictedCharacter = restrictedCharactersPattern.test(PageCode);
        const result = {
            isValid: !!PageCode && !hasRestrictedCharacter && PageCode.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !PageCode ? requiredErrorMessage :
                (hasRestrictedCharacter ?
                    'Special characters are not allowed' :
                    maxLengthErrorMessage);
        }

        return result;
    }

    _validatePageName() {
        const { PageName } = this.state.data;

        const result = {
            isValid: !!PageName && PageName.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !PageName ? requiredErrorMessage : maxLengthErrorMessage;
        }

        return result;
    }

    render() {
        const { onCancel } = this.props;
        const { PageName, PageCode } = this.state.data;

        const pageNameValidation = this._validatePageName();
        const pageCodeValidation = this._validatePageCode();
        const isValid = pageNameValidation.isValid && pageCodeValidation.isValid;

        return (
            <div className="landing-page-form-container">
                <Form tag="div" className="mb-5">
                    <FormGroup row className="mb-2">
                        <Label for="PageName" sm={7} className="required">Landing Page Name (Shown in dropdown)</Label>
                        <Col sm={5}>
                            <Input type="text" name="PageName" id="PageName"
                                value={PageName} onChange={this.handleChange} invalid={!pageNameValidation.isValid} />
                            {pageNameValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageNameValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="PageCode" sm={7} className="required">Landing Page Code (Contained in links)</Label>
                        <Col sm={5}>
                            <Input type="text" name="PageCode" id="PageCode"
                                value={PageCode} onChange={this.handleChange} invalid={!pageCodeValidation.isValid} />
                            {pageCodeValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: pageCodeValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                </Form>

                <Button type="button" color="success" className="mr-3" disabled={!isValid} onClick={this.handleSave}>Submit</Button>
                <Button type="button" color="info" onClick={onCancel}>Cancel</Button>
            </div>
        )
    }
}