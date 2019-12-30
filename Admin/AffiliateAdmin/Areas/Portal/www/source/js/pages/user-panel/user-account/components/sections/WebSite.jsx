import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

import { capitalizeFirstChar } from '../../modules/service';
import { setUserAccountInfo, setMoreSiteUrlsList } from '../../actions';
import { NotificationPopup } from '../../../../common/components/NotificationPopup';

export class WebSite extends PureComponent {
    constructor(props) {
        super(props);

        this.moreSiteUrlRefs = [];

        for (let i = 0; i < this.props.moreSiteUrlsList.length; i++) {
            this.moreSiteUrlRefs.push(React.createRef());
        }

        this.state = {
            messageNotification: ''
        }
    }

    handleChange = (event) => {
        const target = event.target;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: target.value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    handleChangeMoreUrl = (event) => {
        const { moreSiteUrlsList } = this.props;
        const { name, value } = event.target;
        moreSiteUrlsList[name] = value;

        this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    }

    handleAddUrl = () => {
        const { moreSiteUrlsList } = this.props;
        const newMoreSiteUrlsList = moreSiteUrlsList.map(siteUrl => siteUrl.trim());
        const findIndex = newMoreSiteUrlsList.indexOf('');

        if (findIndex === -1) {
            this.moreSiteUrlRefs.push(React.createRef());
            moreSiteUrlsList.push('');
            this.props.setMoreSiteUrlsList(moreSiteUrlsList);
        } else {
            this.moreSiteUrlRefs[findIndex].current.focus();
            this.setState({ messageNotification: `Please input Website URL ${findIndex + 2}` });
        }
    }

    handleDeleteUrl = (siteIndex) => {
        const { moreSiteUrlsList } = this.props;
        moreSiteUrlsList.splice(siteIndex, 1);
        this.moreSiteUrlRefs.splice(siteIndex, 1);

        this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    }

    _renderMoreSiteUrl(moreSiteUrlsList) {
        return (
            <React.Fragment>
                {
                    moreSiteUrlsList.map((value, index) => {
                        return (
                            <FormGroup row key={`siteUrl${index}`}>
                                <Label for={`${index}`} sm={3} xl={2} className="required">Website URL {index + 2}</Label>
                                <Col sm={5} xl={6}>
                                    <input className="form-control" name={index} value={value} onChange={this.handleChangeMoreUrl} ref={this.moreSiteUrlRefs[index]} />
                                </Col>
                                <Button outline color="danger" className="mb-2" onClick={() => this.handleDeleteUrl(index)}><span aria-hidden>Del</span></Button>
                            </FormGroup>
                        )
                    })
                }
            </React.Fragment>
        );
    }

    render() {
        const { messageNotification } = this.state;
        const { userAccountInfo, moreSiteUrlsList } = this.props;

        return (
            <React.Fragment>
                <Form tag="div">
                    <FormGroup row>
                        <Label for="siteUrl" sm={3} xl={2} className="required">Website URL</Label>
                        <Col sm={5} xl={6}>
                            <Input type="text" name="siteUrl" value={userAccountInfo.SiteUrl} onChange={this.handleChange} />
                        </Col>
                        <Button outline color="secondary" className="mb-2" onClick={this.handleAddUrl}>Add Url</Button>
                    </FormGroup>
                    {
                        this._renderMoreSiteUrl(moreSiteUrlsList)
                    }
                    <FormGroup row>
                        <Label for="noSiteDescription" sm={3} xl={2}>I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL .</Label>
                        <Col sm={6} xl={7}>
                            <Input type="textarea" style={{ height: 70 }} id="noSiteDescription" name="noSiteDescription" value={userAccountInfo.NoSiteDescription} onChange={this.handleChange} />
                        </Col>
                    </FormGroup>
                </Form>
                {
                    !!messageNotification && <NotificationPopup
                        message={messageNotification}
                        isConfirmPopup={false}
                        onClose={() => this.setState({ messageNotification: '' })}
                    />
                }
            </React.Fragment>
        )
    }
}

function mapStateToProps({ edit }) {
    const { userAccountInfo, moreSiteUrlsList } = edit;
    return Object.assign({}, { userAccountInfo, moreSiteUrlsList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo, setMoreSiteUrlsList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(WebSite);