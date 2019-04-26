import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col } from 'reactstrap';

import { updateLanguage, setLanguage, changePassword } from '../actions';
import { getAffiliateInfo } from '../../mainpage/actions/loadingDataAction';

import { status, resource } from '../modules/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import { toggleLoader } from '../../../../actions/loaderAction';

import { ChangePasswordForm } from '../../../affiliate-info/components/ChangePasswordForm';

export class GeneralInfo extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isChangingLanguage: false,
            isChangingPassword: false
        }
    }

    handleChangeLanguage = async (event) => {
        const isConfirmed = window.confirm("Are you sure you want to change language for this affiliate?");
        if (isConfirmed) {
            const preferredLanguage = event.target.value;
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, PreferredLanguage: preferredLanguage });
            const data = await this.props.updateLanguage(param);

            if (data.IsSuccessful) {
                window.alert("The language has been updated successfully!");
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(data.Message);
            }
        }
    }

    clickChangeLanguage = () => {
        this.setState({ isChangingLanguage: true });
    }

    handleBlurLanguage = () => {
        this.setState({ isChangingLanguage: false });
    }

    toggleChangingPassword = (isChanging) => {
        this.setState({ isChangingPassword: isChanging })
    }

    _renderLanguage() {
        const { isChangingLanguage } = this.state;
        const { affiliateInfo, languageList } = this.props;

        return (
            <React.Fragment>
                <select style={{ display: isChangingLanguage ? 'inline-block' : 'none' }}
                    defaultValue={affiliateInfo.PreferredLanguage} onChange={this.handleChangeLanguage} onBlur={this.handleBlurLanguage}>
                    {
                        languageList.map(language => {
                            return (<option value={language.LanguageCode} key={`affinfo-lang-${language.LanguageCode}`}>{language.LanguageName}</option>)
                        })
                    }
                </select>
                <span className='popup' style={{ display: isChangingLanguage ? 'none' : 'block' }}
                    onClick={this.clickChangeLanguage}>
                    {
                        languageList.map(language => {
                            return affiliateInfo.PreferredLanguage === language.LanguageCode && language.LanguageName
                        })
                    }
                </span>
            </React.Fragment>
        )
    }

    _renderApprovedStatusInfo() {
        const { isChangingPassword } = this.state;
        const { affiliateInfo, affName } = this.props;
        return (
            <React.Fragment>
                <Row>
                    <Col sm={5}>{resource.Password}</Col>
                    <Col sm={7} className='text-right text-primary'>
                        <span className='popup' onClick={() => this.toggleChangingPassword(true)}>[{resource.Change}]</span>
                        {affiliateInfo && <ChangePasswordForm
                            showPopupPassword={isChangingPassword}
                            affId={affiliateInfo.AffId}
                            setShowPopupPassword={this.toggleChangingPassword}
                            toggleLoader={toggleLoader}
                        />}
                    </Col>
                </Row>
                <a href={resolveClientUrl(`Portal/Member?txtAffUsername=${affName}`)} target='fMain'>{resource.MemberList}</a>
            </React.Fragment>
        )
    }

    render() {
        const { affiliateInfo } = this.props;
        const isApproved = affiliateInfo && affiliateInfo.Status.toLowerCase() === status.Approved;
        const siteUrl = affiliateInfo && affiliateInfo.SiteUrl.indexOf('http://') === -1 ? 'http://' + affiliateInfo.SiteUrl : affiliateInfo.SiteUrl;
        const arryContactApps = JSON.parse(affiliateInfo && affiliateInfo.ContactApps);

        return affiliateInfo && (
            <div className="general-info pt-2">
                <Row className="border-0 py-1">
                    <Col sm={5}>
                        <span className='text_bold'>{resource.Affiliate}</span>
                    </Col>
                    <Col sm={7} className='text-right d-inline-block'>
                        {
                            isApproved && !affiliateInfo.Closed ?
                                (<a href="#" className='upper b' target='_blank'>{affiliateInfo.AffName}</a>) :
                                (<span>{affiliateInfo.AffName}</span>)
                        }{' '}
                        [<span id="affID">{affiliateInfo.AffId}</span>]
                    </Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.FirstName}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.FirstName}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.LastName}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.LastName}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.Gender}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.Gender === null ? '' : (affiliateInfo.Gender === true ? resource.Male : resource.Female)}</Col>
                </Row>
                <Row>
                    <Col sm={5} className="pr-0">{resource.ContactNumber}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.Phone}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{arryContactApps && arryContactApps[0].Name}</Col>
                    <Col sm={7} className='text-right'>{arryContactApps && arryContactApps[0].Value}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.Language}</Col>
                    <Col sm={7} className='text-right text-primary'>{this._renderLanguage()}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.WebsiteUrl}</Col>
                    <Col sm={7} className='text-right text-truncate'>
                        <a target='_blank' title={siteUrl} href={`http://anonym.to/?${siteUrl}`}>
                            {affiliateInfo.SiteUrl.replace('http://', '').replace('www.', '')}
                        </a>
                    </Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.LastLogin}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.LastLoginDateText}</Col>
                </Row>
                <Row>
                    <Col sm={5}>{resource.LastLoginIp}</Col>
                    <Col sm={7} className='text-right'>
                        <a target='_blank' href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${affiliateInfo.LastLoginIp}&j=1`}>
                            {affiliateInfo.LastLoginIp}
                        </a>
                    </Col>
                </Row>
                {
                    isApproved && this._renderApprovedStatusInfo()
                }
            </div>
        )
    }
}

function mapStateToProps({ affCommonInfo, leftPanel }) {
    const { affiliateInfo, languageList } = leftPanel;
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, languageList, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateLanguage, getAffiliateInfo, setLanguage, changePassword }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralInfo);