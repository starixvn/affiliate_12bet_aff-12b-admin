import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

import { capitalizeFirstChar } from '../../modules/service';
import { setUserAccountInfo, setMoreSiteUrlsArray } from '../../actions';

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
        let { moreSiteUrlsArray } = this.props;
        const target = event.target;
        moreSiteUrlsArray[target.name] = target.value;

        this.props.setMoreSiteUrlsArray(moreSiteUrlsArray);
    }

    handleAddUrl = () => {
        let { moreSiteUrlsArray } = this.props;
        const findIndex = moreSiteUrlsArray.indexOf('');

        if(findIndex === -1) {
            moreSiteUrlsArray.push('');
            this.props.setMoreSiteUrlsArray(moreSiteUrlsArray);
        } else {
            window.alert(`Please input Website URL ${findIndex + 2}`);
            document.getElementById(findIndex).focus();
        }
    }

    handleDeleteUrl = (siteIndex) => {
        let { moreSiteUrlsArray } = this.props;
        moreSiteUrlsArray.splice(siteIndex, 1);

        this.props.setMoreSiteUrlsArray(moreSiteUrlsArray);
    }

    _renderMoreSiteUrl(moreSiteUrlsArray) {
        return (
            <React.Fragment>
                {
                    moreSiteUrlsArray.map((value, index) => {
                        return (
                            <FormGroup row>
                                <Label for={index} sm={3} xl={2}>Website URL {index + 2}</Label>
                                <Col sm={5} xl={6}>
                                    <Input type="text" id={index} name={index} value={value} onChange={this.handleChangeMoreUrl}/>
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
        const { userAccountInfo, moreSiteUrlsArray } = this.props;

        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="siteUrl" sm={3} xl={2}>Website URL</Label>
                    <Col sm={5} xl={6}>
                        <Input type="text" id="siteUrl" name="siteUrl" value={userAccountInfo.SiteUrl} onChange={this.handleChange}/>
                    </Col>
                    <Button color="primary" onClick={this.handleAddUrl}><span aria-hidden>Add Url</span></Button>
                </FormGroup>
                {
                    moreSiteUrlsArray && moreSiteUrlsArray[0] && this._renderMoreSiteUrl(moreSiteUrlsArray)
                }
                <FormGroup row>
                    <Label for="noSiteDescription" sm={3} xl={2}>I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL .</Label>
                    <Col sm={6} xl={7}>
                        <Input type="textarea" style={{ height: 70 }} id="noSiteDescription" name="noSiteDescription" value={userAccountInfo.NoSiteDescription} onChange={this.handleChange}/>
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}

function mapStateToProps({ userPanel }) {
    const { userAccountInfo, moreSiteUrlsArray } = userPanel;
    return Object.assign({}, { userAccountInfo, moreSiteUrlsArray });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo, setMoreSiteUrlsArray }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(WebSite);