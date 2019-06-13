import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Button, Row, Col, Alert } from 'reactstrap';

import { getUserAccountInfo, updateUserAccount, getAffiliateInfo } from '../actions';
import { validateUserAccount } from '../modules/validation';
import { userPanelMenuKey } from '../modules/constants';

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
        const { affId } = this.props;
        affId && this.props.getUserAccountInfo(affId);
    }

    handleGetMoreSiteUrls = (moreSiteUrlsList) => {
        return moreSiteUrlsList && moreSiteUrlsList.map(siteUrl => siteUrl.trim()).join(';');
    }

    handleTrimObject = (objectOriginal) => {
        return Object.keys(objectOriginal).reduce((accumulator, key) => {
            let item = objectOriginal[key];
            if (typeof item === 'string') {
                item = item.trim();
            }
            accumulator[key] = item;

            return accumulator;
        }, {});
    }

    handleSubmit = async () => {
        const { affName, userAccountInfo, moreSiteUrlsList } = this.props;
        const userAccountInfoAfterTrim = this.handleTrimObject(userAccountInfo);
        const resultValidateUserAccount = validateUserAccount(userAccountInfoAfterTrim, moreSiteUrlsList);

        window.scrollTo({ top: 0, behavior: "smooth" });

        if (resultValidateUserAccount.isValid) {
            const moreSiteUrls = this.handleGetMoreSiteUrls(moreSiteUrlsList);
            const newUserAccountInfo = Object.assign({}, userAccountInfoAfterTrim, { MoreSiteUrls: moreSiteUrls });

            const result = await this.props.updateUserAccount(newUserAccountInfo);

            if (result) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: result.IsSuccessful,
                    message: result.IsSuccessful ? `${userAccountInfo.AffName} has been updated successfully!` : result.Message,
                }, () => window.setTimeout(() => {
                    this.hideMessage();
                    result.IsSuccessful && this.props.getAffiliateInfo(affName, userPanelMenuKey.edit)
                }, 3000));
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
                isShowMessage: true,
                message: resultValidateUserAccount.message,
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
                            {
                                this.props.moreSiteUrlsList && <WebSite />
                            }
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