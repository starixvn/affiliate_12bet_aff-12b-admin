import React, { PureComponent } from 'react';
import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';

const initialState = {
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
    isShowMessage: false,
    isSuccess: true,
    message: ''
};

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = { ...initialState }
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _handleSubmit = () => {
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

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit();
        }
    }

    _handleReset = () => {
        this.setState({ ...initialState });
    }

    render() {
        const { oldPassword, newPassword, confirmPassword, isShowMessage, isSuccess, message } = this.state;

        return (
            <div className="change-admin-password-container">
                <fieldset>
                    <legend><h6 className="font-weight-bold">Change Admin Password</h6></legend>
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Form tag="form" className="mb-2" autocomplete="off">
                        <FormGroup row className="my-2">
                            <Label for="oldPassword" sm={4} className="text-right">Old Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required" name="oldPassword" id="oldPassword" autocomplete="false"
                                    value={oldPassword} onChange={this._handleChange} />
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-2">
                            <Label for="newPassword" sm={4} className="text-right">New Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required" name="newPassword" id="newPassword" autocomplete="false"
                                    value={newPassword} onChange={this._handleChange} />
                            </Col>
                        </FormGroup>

                        <FormGroup row className="mb-2">
                            <Label for="confirmPassword" sm={4} className="text-right">Confirm Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required" name="confirmPassword" id="confirmPassword" autocomplete="false"
                                    value={confirmPassword} onChange={this._handleChange} />
                            </Col>
                        </FormGroup>
                    </Form>
                </fieldset>

                <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>Submit</Button>
                <Button type="button" color="info" onClick={this._handleReset}>Reset</Button>
            </div>
        )
    }
}