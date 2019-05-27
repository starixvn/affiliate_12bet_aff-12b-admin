import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Button, Row, Col, Alert } from 'reactstrap';

import { getUserAccountInfo, updateUserAccount } from '../actions';
import { messageList, regexEmail } from '../modules/constants';
import { countriesList } from '../modules/constants';

import Section from './Section';
import Personal from './sections/Personal';
import Additional from './sections/Additional';
import EmailFormat from './sections/EmailFormat';
import WebSite from './sections/WebSite';
import Payment from './sections/Payment';
import { getAffiliateInfo } from '../../../customer/mainpage/actions/loadingDataAction';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

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
        const { affId } = this.props;
        affId && this.props.getUserAccountInfo(affId);
    }

    getMessage = (message, fieldName, value) => {
        return message = value == '' ? message + messageList[fieldName] + "<br />" : message;
    }

    validateMoreSiteUrls = (moreSiteUrlsList, message, isValid) => {
        const moreSiteUrls = moreSiteUrlsList.map(siteUrl => siteUrl.trim());
        const emptySiteUrlIndex = moreSiteUrls.indexOf('');
        const isNotEmptySiteUrl = emptySiteUrlIndex == -1;
        message = isNotEmptySiteUrl ? message : message + `Website URL ${emptySiteUrlIndex + 2} is required<br />`;
        return {
            message,
            isValid: isValid && isNotEmptySiteUrl
        }
    }

    validatePaymentMethod = (userAccountInfo, result) => {
        if (userAccountInfo.PreferredPayment === 'Player') {
            result.message = this.getMessage(result.message, 'PlayerUsername', userAccountInfo.PlayerUsername);
            result.isValid = result.isValid && userAccountInfo.PlayerUsername != '';
        }

        if (userAccountInfo.PreferredPayment === 'Neteller') {
            result.message = this.getMessage(result.message, 'NetellerAccountId', userAccountInfo.NetellerAccountId);
            result.isValid = result.isValid && userAccountInfo.NetellerAccountId != '';
        }

        if (userAccountInfo.PreferredPayment === 'Moneybookers') {
            result.message = this.getMessage(result.message, 'MoneybookersEmail', userAccountInfo.MoneybookersEmail);
            result.isValid = result.isValid && userAccountInfo.MoneybookersEmail != '';
        }

        if (userAccountInfo.PreferredPayment === 'Wire') {
            result.message = this.getMessage(result.message, 'BankName', userAccountInfo.BankName);
            result.message = this.getMessage(result.message, 'BankAddress', userAccountInfo.BankAddress);
            result.message = this.getMessage(result.message, 'BankAccountName', userAccountInfo.BankAccountName);
            result.message = this.getMessage(result.message, 'BankAccountNumber', userAccountInfo.BankAccountNumber);
            result.isValid = result.isValid && userAccountInfo.BankName != '' && userAccountInfo.BankAddress != '' &&
                userAccountInfo.BankAccountName != '' && userAccountInfo.BankAccountNumber != '';
        }

        return result;
    }

    validateUserAccount = (userAccountInfo, moreSiteUrlsList) => {
        let message = '';
        const checkEmail = new RegExp(regexEmail);
        const isValidGender = userAccountInfo.Gender != null;
        const isValidEmail = checkEmail.test(userAccountInfo.Email);
        const country = countriesList[userAccountInfo.Country] === undefined ? "" : userAccountInfo.Country;

        message = this.getMessage(message, 'FirstName', userAccountInfo.FirstName);
        message = this.getMessage(message, 'LastName', userAccountInfo.LastName);
        message = isValidGender ? message : message + messageList.Gender + "<br />";
        message = isValidEmail ? message : message + messageList.Email + "<br />";
        message = this.getMessage(message, 'Phone', userAccountInfo.Phone);
        message = this.getMessage(message, 'Address', userAccountInfo.Address);
        message = this.getMessage(message, 'City', userAccountInfo.City);
        message = this.getMessage(message, 'Postcode', userAccountInfo.Postcode);
        message = this.getMessage(message, 'Country', country);
        message = this.getMessage(message, 'SiteUrl', userAccountInfo.SiteUrl);

        let isValid = isValidGender && isValidEmail && userAccountInfo.FirstName != '' && userAccountInfo.LastName != '' &&
            userAccountInfo.Phone != '' && userAccountInfo.Address != '' && userAccountInfo.City != '' &&
            userAccountInfo.Postcode != '' && userAccountInfo.Country != '' && userAccountInfo.SiteUrl != '';

        const result = moreSiteUrlsList ? this.validateMoreSiteUrls(moreSiteUrlsList, message, isValid) : ({ message, isValid });

        return this.validatePaymentMethod(userAccountInfo, result);
    }

    handleGetMoreSiteUrls = (moreSiteUrlsList) => {
        const moreSiteUrls = moreSiteUrlsList && moreSiteUrlsList.reducer((accumulator, siteUrl) => accumulator +=  siteUrl.trim() + ";", '');

        return moreSiteUrls.slice(0, -1);
    }

    handleSubmit = async () => {
        const { affId, affName, userAccountInfo, moreSiteUrlsList } = this.props;

        const userAccountInfoAfterTrim = Object.keys(userAccountInfo).reduce((accumulator, key) => {
            let item = userAccountInfo[key];
            if (typeof item === 'string') {
                item = item.trim();
            }
            accumulator.push(item);
        }, [])

        const validateUserAccount = this.validateUserAccount(userAccountInfoAfterTrim, moreSiteUrlsList);

        window.scrollTo({ top: 0, behavior: "smooth" });

        if (validateUserAccount.isValid) {
            const moreSiteUrls = this.handleGetMoreSiteUrls(moreSiteUrlsList);
            const newUserAccountInfo = Object.assign({}, userAccountInfoAfterTrim, { MoreSiteUrls: moreSiteUrls });

            const result = await this.props.updateUserAccount(newUserAccountInfo);

            if (result) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: result.IsSuccessful,
                    message: result.IsSuccessful ? `${userAccountInfo.AffName} has been updated successfully!` : result.Message,
                }, () => window.setTimeout(this.hideMessage, 3000));

                if (result.IsSuccessful) {
                    this.props.getUserAccountInfo(affId);
                    window.setTimeout(() => this.props.getAffiliateInfo(affName, userPanelMenuKey.edit), 3000);
                }
            }
            else {
                this.setState({
                    isSuccess: false,
                    isShowMessage: true,
                    message: 'Update is failed!',
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: !validateUserAccount.isValid,
                message: validateUserAccount.message,
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
                    <Col xs="6" className="pl-0">
                        <Section title="Personal Information">
                            <Personal />
                        </Section>
                    </Col>
                    <Col xs="6" className="pr-0">
                        <Section title="Additional Information">
                            <Additional />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12" className="px-0">
                        <Section title="Email Format Preference">
                            <EmailFormat />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12" className="px-0">
                        <Section title="Website Information">
                            <WebSite />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12" className="px-0">
                        <Section title="Payment Information">
                            <Payment />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Button className="mb-3" color="success" onClick={this.handleSubmit}>Submit</Button>
                </Row>
            </Container>
        );
    }
}

function mapStateToProps({ affCommonInfo, edit }) {
    const { affId, affName } = affCommonInfo;
    const { userAccountInfo, moreSiteUrlsList } = edit;

    return Object.assign({}, { affId, affName, userAccountInfo, moreSiteUrlsList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getUserAccountInfo, updateUserAccount, getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);