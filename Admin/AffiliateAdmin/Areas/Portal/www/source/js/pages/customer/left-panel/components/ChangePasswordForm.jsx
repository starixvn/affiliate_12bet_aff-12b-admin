import React, { PureComponent } from 'react';

export default class ChangePasswordForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            password: '',
            confirmPassword: '',
            isShowMessage: false,
            message: '',
            statusClass: ''
        }
    }

    handleChange = ({ target }) => {
        const { name, value } = target;

        this.setState({ [name]: value });
    }

    handleSubmit = () => {
        const { password, confirmPassword } = this.state;
        const { onSubmit, onClose } = this.props;
        const { resource } = window;

        if (password === '') {
            this.setState({ isShowMessage: true, message: resource.requirePassword });
        }
        else if (password !== confirmPassword) {
            this.setState({ isShowMessage: true, message: resource.errorConfirmPassword });
        }
        else {
            const param = { password, confirmPassword };
            onSubmit(param).then(response => {
                const result = response.data;
                const message = result.IsSuccessful ? resource.changePassSuccess : result.Message;

                this.setState({ isShowMessage: true, message, statusClass: result.IsSuccessful ? 'done' : '' }, () => {
                    window.setTimeout(() => onClose(), 2500);
                });
            })
        }
    }

    render() {
        const { onClose } = this.props;
        const { message, isShowMessage, statusClass } = this.state;
        const className = (isShowMessage ? 'd-block' : 'd-none') + ' ' + statusClass;

        return (
            <div className="pad7">
                <div id="errMsg" className={className}>
                    <div id="error_item">
                        <ul>
                            <li><a className="err-msg" id="error_msg">{message}</a></li>
                        </ul>
                    </div>
                </div>

                <div className="pcontent">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td className="text-right text-bold">Password:</td>
                                <td>
                                    <input id="password" type="password" name="password" className="textbox" onChange={this.handleChange} />
                                </td>
                            </tr>
                            <tr>
                                <td className="r b">Confirm Password:</td>
                                <td>
                                    <input id="confirmPassword" type="password" name="confirmPassword" className="textbox" onChange={this.handleChange} />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div className="bottombutton">
                    <input type="button" className="buttonlogin" value="Submit" onClick={this.handleSubmit} /> {'  '}
                    <input type="button" className="buttonlogin" value="Cancel" onClick={onClose} />
                </div>
            </div>
        )
    }
}