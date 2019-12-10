import React, { PureComponent } from 'react';
import { Alert, Form, FormGroup, Label, Input, Col } from 'reactstrap';

import { post } from '../../../modules/ajaxRequest';
import { resolveClientUrl } from '../../../modules/utils';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        const now = new Date();
        this.state = {
            userName: '',
            password: '',
            isShowMessage: false,
            message: '',
            year: now.getFullYear()
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

    _validateAccount = (userName, password) => {
        const isValidUserName = !!userName;
        if (isValidUserName && !!password) {
            return true;
        }

        this._handleShowAlert(false, !isValidUserName ? 'User Name is empty. Please check!' : 'Password is empty. Please check!');
        return false;
    }
    _handleLogin = async () => {
        const { userName, password } = this.state;

        const isValidAccount = this._validateAccount(userName, password);

        if (isValidAccount) {
            const param = { userName, password };
            const response = await post('Portal/Auth/Login', param);

            if (response) {
                const { data } = response;

                if (data.IsSuccessful) {
                    window.location = resolveClientUrl("Portal/Home")
                } else {
                    this._handleShowAlert(false, data.Message);
                }
            } else {
                this._handleShowAlert(false, "An error occurred while processing your request");
            }
        }
    }

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleLogin();
        }
    }

    _handleChange = (event) => {
        const target = event.target;

        this.setState({ [target.name]: target.value });
    }

    render() {
        const { userName, password, isShowMessage, message, year } = this.state;

        return (
            <div className="login-container">
                <div className="lgRight"></div>
                <div className="lgRp">
                    <div className="logo"></div>
                    <div className="war-box">
                        <Alert color='danger' isOpen={isShowMessage}>
                            <div dangerouslySetInnerHTML={{ __html: message }}></div>
                        </Alert>
                    </div>
                    <Form tag="form" className="form-login pl-3" autocomplete="false">
                        <FormGroup row className="mb-1">
                            <span className="icUser">&nbsp;</span>
                            <Label for="userName" sm={3} className="form-login-text">User Name</Label>
                            <Col sm={6}>
                                <Input type="text" name="userName" id="userName" value={userName}
                                    onKeyPress={this._handlePressEnter} onChange={this._handleChange} />
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <span className="icPass">&nbsp;</span>
                            <Label for="passWord" sm={3} className="form-login-text">Password</Label>
                            <Col sm={6}>
                                <Input type="password" name="password" id="password" autocomplete="false"
                                    value={password} onKeyPress={this._handlePressEnter} onChange={this._handleChange} />
                            </Col>
                        </FormGroup>
                    </Form>
                    <div className="bt">
                        <a href="javascript:;" class="bt-login" onClick={this._handleLogin}>Log On</a>
                    </div>
                    <div className="foot">
                        <span className="foot-left">&nbsp;</span>
                        <span className="foot-rp">{`© Copyright ${year} Pacific Sea Invests`}</span>
                        <span className="foot-right">&nbsp;</span>
                        <span className="foot-text">Best view in</span>
                        <span className="foot-icIE">&nbsp;</span>
                        <span className="foot-icFirefox">&nbsp;</span>
                    </div>
                </div>
            </div>
        )
    }
}