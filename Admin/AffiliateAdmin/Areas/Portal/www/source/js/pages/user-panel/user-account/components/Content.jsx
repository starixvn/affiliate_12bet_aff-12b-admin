import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Button, Row, Col, Alert } from 'reactstrap';

import { getUserAccountInfo, updateUserAccount } from '../actions';
import { messageList, regexEmail } from '../modules/constants';

import Section from './Section';
import Personal from './sections/Personal';
import Additional from './sections/Additional';
import EmailFormat from './sections/EmailFormat';
import WebSite from './sections/WebSite';
import Payment from './sections/Payment';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isSuccess: true,
            isShowMessage: false,
            message: ''
        }
    }

    componentDidMount() {
        const { affId, getUserAccountInfo } = this.props;
        affId && getUserAccountInfo(affId);
    }

    getMessage = (message, fieldName, value) => {
        return message = value == '' ? message + messageList[fieldName] + "<br />" : message;
    }

    validateMoreSiteUrls = (moreSiteUrlsArray, message, isValid) => {
        const moreSiteUrls = moreSiteUrlsArray.map(siteUrl => siteUrl.trim());
        const emptySiteUrlIndex = moreSiteUrls.indexOf('');
        const isNotEmptySiteUrl = emptySiteUrlIndex == -1;
        message = isNotEmptySiteUrl ? message : message + `Website URL ${emptySiteUrlIndex + 2} is required<br />`;
        return {
            message,
            isValid: isValid && isNotEmptySiteUrl
        }
    }

    validateSelectedPaymentMethod = (userAccountInfo, selectedPaymentMethod, result) => {
        if(selectedPaymentMethod === 'Player') {
            result.message = this.getMessage(result.message, 'PlayerUsername', userAccountInfo.PlayerUsername);
            result.isValid = result.isValid && userAccountInfo.PlayerUsername != '';
        }

        if(selectedPaymentMethod === 'Neteller') {
            result.message = this.getMessage(result.message, 'NetellerAccountId', userAccountInfo.NetellerAccountId);
            result.isValid = result.isValid && userAccountInfo.NetellerAccountId != '';
        }

        if(selectedPaymentMethod === 'Moneybookers') {
            result.message = this.getMessage(result.message, 'MoneybookersEmail', userAccountInfo.MoneybookersEmail);
            result.isValid = result.isValid && userAccountInfo.MoneybookersEmail != '';
        }

        if(selectedPaymentMethod === 'Wire') {
            result.message = this.getMessage(result.message, 'BankName', userAccountInfo.BankName);
            result.message = this.getMessage(result.message, 'BankAddress', userAccountInfo.BankAddress);
            result.message = this.getMessage(result.message, 'BankAccountName', userAccountInfo.BankAccountName);
            result.message = this.getMessage(result.message, 'BankAccountNumber', userAccountInfo.BankAccountNumber);
            result.isValid = result.isValid && userAccountInfo.BankName != '' && userAccountInfo.BankAddress != '' &&
                                userAccountInfo.BankAccountName != '' && userAccountInfo.BankAccountNumber != '';
        }

        return result;
    }

    validateUserAccount = (userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod) => {
        const checkEmail = new RegExp(regexEmail);
        let message = '';
        const isValidGender = userAccountInfo.Gender != null;
        const isValidEmail = checkEmail.test(userAccountInfo.Email);

        message = this.getMessage(message, 'FirstName', userAccountInfo.FirstName);
        message = this.getMessage(message, 'LastName', userAccountInfo.LastName);
        message = isValidGender ? message : message + messageList.Gender + "<br />";
        message = isValidEmail ? message : message  + messageList.Email + "<br />";
        message = this.getMessage(message, 'Phone', userAccountInfo.Phone);
        message = this.getMessage(message, 'Address', userAccountInfo.Address);
        message = this.getMessage(message, 'City', userAccountInfo.City);
        message = this.getMessage(message, 'Postcode', userAccountInfo.Postcode);
        message = this.getMessage(message, 'Country', userAccountInfo.Country);
        message = this.getMessage(message, 'SiteUrl', userAccountInfo.SiteUrl);

        let isValid = isValidGender && isValidEmail && userAccountInfo.FirstName != '' && userAccountInfo.LastName != '' &&
                        userAccountInfo.Phone != '' && userAccountInfo.Address != '' && userAccountInfo.City != '' &&
                        userAccountInfo.Postcode != '' && userAccountInfo.Country != '' && userAccountInfo.SiteUrl != '';

        const result = this.validateMoreSiteUrls(moreSiteUrlsArray, message, isValid);

        return this.validateSelectedPaymentMethod(userAccountInfo, selectedPaymentMethod, result);
    }

    handleSubmit = async () => {
        const { affId, userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod } = this.props;
        const userAccountInfoAfterTrim = Object.assign({}, userAccountInfo)
        Object.keys(userAccountInfoAfterTrim).map(key => userAccountInfo[key] = typeof userAccountInfo[key] === 'string' ? userAccountInfo[key].trim() : userAccountInfo[key]);

        const validateUserAccount = this.validateUserAccount(userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod);

        if(validateUserAccount.isValid) {
            const newUserAccountInfo = Object.assign({}, userAccountInfo, { MoreSiteUrls: moreSiteUrlsArray.toString() });

            const data = await this.props.updateUserAccount(newUserAccountInfo);

            if (data) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: data.IsSuccessful,
                    message: data.IsSuccessful ? `${userAccountInfo.UserName} has been updated successfully!` : data.Message,
                }, () => window.setTimeout(this.hideMessage, 3000));

                this.props.getUserAccountInfo(affId);
            }
            else {
                this.setState({
                    isSuccess: false,
                    isShowMessage: true,
                    message: 'Update User Account is failed!',
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: !result.isValid,
                message: result.message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { isSuccess, isShowMessage, message } = this.state;

        return this.props.userAccountInfo && (
            <Container fluid={true} className="user-account-container">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <Row>
                    <Col xs="6">
                        <Section title="Personal Information">
                            <Personal />
                        </Section>
                    </Col>
                    <Col xs="6">
                        <Section title="Additional Information">
                            <Additional />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Email Format Preference">
                            <EmailFormat />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Website Information">
                            <WebSite />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Payment Information">
                            <Payment />
                        </Section>
                    </Col>
                </Row>

                <Button color="success" onClick={this.handleSubmit}>Submit</Button>
            </Container>
        );
    }
}

function mapStateToProps({ affCommonInfo, userPanel }) {
    const { affId } = affCommonInfo;
    const { userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod } = userPanel;
    return Object.assign({}, { affId, userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getUserAccountInfo, updateUserAccount }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);