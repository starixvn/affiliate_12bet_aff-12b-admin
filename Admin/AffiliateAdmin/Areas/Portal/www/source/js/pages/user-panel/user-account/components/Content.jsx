import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Button, Row, Col, Alert } from 'reactstrap';

import { getUserAccountInfo, updateUserAccount } from '../actions';

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

    handleSubmit = async () => {
        const { userAccountInfo, moreSiteUrlsArray, selectedPaymentMethod } = this.props;

        const newUserAccountInfo = Object.assign({}, userAccountInfo, { MoreSiteUrls: JSON.stringify(moreSiteUrlsArray) });

        const data = await this.props.updateUserAccount(newUserAccountInfo);

        if (data) {
            this.setState({
                isShowMessage: true,
                isSuccess: data.IsSuccessful,
                message: data.IsSuccessful ? `${userAccountInfo.UserName} has been updated successfully!` : data.Message,
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
        else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: 'Update User Account is failed!',
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