import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

export class WebSite extends PureComponent {
    constructor(props) {
        super(props);
    }
    _renderMoreSiteUrl(siteUrlsList) {
        console.log("siteUrlsList");
        return (
            <React.Fragment>
                {
                    siteUrlsList.map((value, index) => {
                        return (
                            <FormGroup row>
                                <Label for="websiteUrl" sm={3} xl={2}>Website URL {index + 2}</Label>
                                <Col sm={5} xl={6}>
                                    <Input type="text" id="websiteUrl" name="websiteUrl" value={value} />
                                </Col>
                                <Button color="primary"><span aria-hidden>Del</span></Button>
                            </FormGroup>
                        )
                    })
                }
            </React.Fragment>
        );
    }

    render() {
        const { userAccountInfo } = this.props;
        const moreSiteUrl = userAccountInfo && userAccountInfo.MoreSiteUrl;
        const siteUrlsList = moreSiteUrl && moreSiteUrl.split(';');

        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="websiteUrl" sm={3} xl={2}>Website URL</Label>
                    <Col sm={5} xl={6}>
                        <Input type="text" id="websiteUrl" name="websiteUrl" value={userAccountInfo.SiteUrl} />
                    </Col>
                    <Button color="primary"><span aria-hidden>Add Url</span></Button>
                </FormGroup>
                {
                    siteUrlsList && siteUrlsList[0] && this._renderMoreSiteUrl(siteUrlsList)
                }
                <FormGroup row>
                    <Label for="websiteDescribe" sm={3} xl={2}>I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL .</Label>
                    <Col sm={6} xl={7}>
                        <Input type="textarea" style={{ height: 70 }} id="websiteDescribe" name="websiteDescribe" value={userAccountInfo.NoSiteDescription} />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}

function mapStateToProps({ userAccountInfo }) {
    return Object.assign({}, { userAccountInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(WebSite);