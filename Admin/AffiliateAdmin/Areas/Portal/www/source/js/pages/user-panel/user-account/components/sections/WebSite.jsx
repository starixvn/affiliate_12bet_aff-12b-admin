import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

import { capitalizeFirstChar } from '../../modules/service';
import { setUserAccountInfo, setMoreSiteUrlsList } from '../../actions';

export class WebSite extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const target = event.target;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: target.value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    handleChangeMoreUrl = (event) => {
        let { moreSiteUrlsList } = this.props;
        const target = event.target;
        moreSiteUrlsList[target.name] = target.value;

        this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    }

    handleAddUrl = () => {
        let { moreSiteUrlsList } = this.props;
        const newMoreSiteUrlsList = moreSiteUrlsList.map(siteUrl => siteUrl.trim());
        const findIndex = newMoreSiteUrlsList.indexOf('');

        if (findIndex === -1) {
            moreSiteUrlsList.push('');
            this.props.setMoreSiteUrlsList(moreSiteUrlsList);
        } else {
            window.alert(`Please input Website URL ${findIndex + 2}`);
            document.getElementById(findIndex).focus();
        }
    }

    handleDeleteUrl = (siteIndex) => {
        let { moreSiteUrlsList } = this.props;
        moreSiteUrlsList.splice(siteIndex, 1);

        this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    }

    _renderMoreSiteUrl(moreSiteUrlsList) {
        return (
            <React.Fragment>
                {
                    moreSiteUrlsList.map((value, index) => {
                        return (
                            <FormGroup row>
                                <Label for={index} sm={3} xl={2} className="required">Website URL {index + 2}</Label>
                                <Col sm={5} xl={6}>
                                    <Input type="text" id={index} name={index} value={value} onChange={this.handleChangeMoreUrl} />
                                </Col>
                                <Button color="primary" onClick={() => this.handleDeleteUrl(index)}><span aria-hidden>Del</span></Button>
                            </FormGroup>
                        )
                    })
                }
            </React.Fragment>
        );
    }

    render() {
        const { userAccountInfo, moreSiteUrlsList } = this.props;

        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="siteUrl" sm={3} xl={2} className="required">Website URL</Label>
                    <Col sm={5} xl={6}>
                        <Input type="text" id="siteUrl" name="siteUrl" value={userAccountInfo.SiteUrl} onChange={this.handleChange} />
                    </Col>
                    <Button color="primary" onClick={this.handleAddUrl}><span aria-hidden>Add Url</span></Button>
                </FormGroup>
                {
                    moreSiteUrlsList && this._renderMoreSiteUrl(moreSiteUrlsList)
                }
                <FormGroup row>
                    <Label for="noSiteDescription" sm={3} xl={2}>I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL .</Label>
                    <Col sm={6} xl={7}>
                        <Input type="textarea" style={{ height: 70 }} id="noSiteDescription" name="noSiteDescription" value={userAccountInfo.NoSiteDescription} onChange={this.handleChange} />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}

function mapStateToProps({ userPanel }) {
    const { userAccountInfo, moreSiteUrlsList } = userPanel;
    return Object.assign({}, { userAccountInfo, moreSiteUrlsList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo, setMoreSiteUrlsList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(WebSite);