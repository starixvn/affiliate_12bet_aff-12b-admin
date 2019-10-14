import React, { PureComponent } from 'react';
import { Alert, Form, FormGroup, Label, Input, Col } from 'reactstrap';

import AppLayout from '../../../layouts/AppLayout';
import Loader from '../../../components/Loader';

import { post } from '../../../modules/ajaxRequest';
import { resolveClientUrl } from './utils';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            passWord: '',
            isShowLoader: false,
            isShowMessage: false,
            message: '',
            now: new Date()
        }
    }

    _toggleLoader(isShow) {
        this.setState({ isShowLoader: !!isShow });
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage, 3000));
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    _validateAccount = (userName, passWord) => {
        const isValidUserName = !!userName;
        if (isValidUserName && !!passWord) {
            return true;
        }

        this._handleShowAlert(false, !isValidUserName ? 'User Name is empty. Please check!' : 'Password is empty. Please check!');
        return false;
    }
    _handleLogin = async () => {
        const { userName, passWord } = this.state;

        const isValidAccount = this._validateAccount(userName, passWord);

        if (isValidAccount) {
            this._toggleLoader(true);

            const param = { userName, passWord };

            const response = await post('Portal/Login/Login', param);

            if (response) {
                if (response.Message = "OK") {
                    location.href = resolveClientUrl("Portal/Home")
                }

                if (response.Message == 'UM') {
                    location.href = resolveClientUrl("UnderMaintenance.aspx");
                }

                this._handleShowAlert(false, response.Message);
            } else {
                this._handleShowAlert(false, "Server Error!");
            }

            this._toggleLoader(false);
        }
    }

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            _handleSearch();
        }
    }

    _handleChange = (event) => {
        const target = event.target;

        this.setState({ [target.name]: target.value });
    }

    render() {
        const { userName, passWord, isShowLoader, isShowMessage, message, now } = this.state;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
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
                                    <Input type="text" className="input-required" name="userName" id="userName" value={userName}
                                        onKeyPress={this._handlePressEnter} onChange={this._handleChange} />
                                </Col>
                            </FormGroup>

                            <FormGroup row>
                                <span className="icPass">&nbsp;</span>
                                <Label for="passWord" sm={3} className="form-login-text">Password</Label>
                                <Col sm={6}>
                                    <Input type="password" className="input-required" name="passWord" id="passWord" autocomplete="false"
                                        value={passWord} onKeyPress={this._handlePressEnter} onChange={this._handleChange} />
                                </Col>
                            </FormGroup>
                        </Form>
                        <div className="bt">
                            <a href="javascript:;" class="bt-login" onClick={this._handleLogin}>Log On</a>
                        </div>
                        <div className="foot">
                            <span className="foot-left">&nbsp;</span>
                            <span className="foot-rp">{`© Copyright ${now.getFullYear()} Pacific Sea Invests`}</span>
                            <span className="foot-right">&nbsp;</span>
                            <span className="foot-text">Best view in</span>
                            <span className="foot-icIE">&nbsp;</span>
                            <span className="foot-icFirefox">&nbsp;</span>
                        </div>
                    </div>
                </div>
            </AppLayout>
        )
    }
}