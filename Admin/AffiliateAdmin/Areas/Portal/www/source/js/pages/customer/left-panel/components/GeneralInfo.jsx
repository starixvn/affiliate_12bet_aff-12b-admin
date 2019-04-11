import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody } from 'reactstrap';

import { updateGeneralInfo, setLanguage, changePassword } from '../actions';

import { status, resource } from '../modules/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import ChangePasswordForm from './ChangePasswordForm';

export class GeneralInfo extends React.Component {
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
            const param = { preferredLanguage };
            const response = await this.props.updateGeneralInfo(param);

            if (response.data.IsSuccessful) {
                this.props.setLanguage(preferredLanguage);
            }
            else {
                window.alert(response.data.Message);
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
                            return (affiliateInfo.PreferredLanguage === language.LanguageCode) && (<option value={language.LanguageCode} key={`language-${language.LanguageCode}`}>{language.LanguageName}</option>)
                        })
                    }
                </span>
            </React.Fragment>
        )
    }

    _renderApprovedStatusInfo() {
        const { isChangingPassword } = this.state;
        const { affiliateInfo, changePassword } = this.props;
        return (
            <React.Fragment>
                <tr style={{ height: 22 }}>
                    <td className='line_table'>{resource.Password}</td>
                    <td className='r line_table'>
                        <span className='popup' onClick={() => this.toggleChangingPassword(true)}>[{resource.Change}]</span>

                        <Modal isOpen={isChangingPassword} backdrop='static'>
                            <ModalHeader toggle={() => this.toggleChangingPassword(false)}>
                                <span className="iconpopup"></span>Change Password
                            </ModalHeader>
                            <ModalBody>
                                <div className="modal-body-wrapper">
                                    {
                                        isChangingPassword &&
                                        <ChangePasswordForm onClose={() => this.toggleChangingPassword(false)}
                                            onSubmit={changePassword} />
                                    }
                                </div>
                            </ModalBody>
                        </Modal>
                    </td>
                </tr>
                <tr style={{ height: 22 }}>
                    <td colSpan='2'>
                        <a href={resolveClientUrl(`Portal/Member?txtAffUsername=${affiliateInfo.AffName}`)} target='fMain'>{resource.MemberList}</a>
                    </td>
                </tr>
            </React.Fragment>
        )
    }

    render() {
        const { affiliateInfo } = this.props;

        const isApproved = affiliateInfo.Status.toLowerCase() === status.Approved;
        const siteUrl = affiliateInfo.SiteUrl.indexOf('http://') === -1 ? 'http://' + affiliateInfo.SiteUrl : affiliateInfo.SiteUrl;

        return (
            <React.Fragment>
                <tr>
                    <td style={{ width: '45%', height: 35 }}>
                        <span className='text_bold'>{resource.Affiliate}</span>
                    </td>
                    <td style={{ width: '55%' }} className='r'>
                        {
                            isApproved && !affiliateInfo.Closed ?
                                (<a href="#" className='upper b' target='_blank'>{affiliateInfo.AffName}</a>) :
                                (<span>{affiliateInfo.AffName}</span>)
                        }{' '}
                        [<span id="affID">{affiliateInfo.AffId}</span>]

                        <input type="hidden" id="hdFname" value={affiliateInfo.FirstName} />
                        <input type="hidden" id="hdPreferLang" value={affiliateInfo.PreferredLanguage} />

                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.FirstName}</td>
                    <td className='line_table r'>{affiliateInfo.FirstName}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.LastName}</td>
                    <td className='line_table r'>{affiliateInfo.LastName}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.Gender}</td>
                    <td className='line_table r'>{affiliateInfo.Gender === null ? '' : (affiliateInfo.Gender === true ? resource.Male : resource.Female)}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.ContactNumber}</td>
                    <td className='line_table r'>{affiliateInfo.Phone}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.Language}</td>
                    <td className='line_table r'>
                        {this._renderLanguage()}
                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.WebsiteUrl}</td>
                    <td className='line_table r'>
                        <a target='_blank' title={siteUrl} href={`http://anonym.to/?${siteUrl}`}>
                            {affiliateInfo.SiteUrl.replace('http://', '').replace('www.', '')}
                        </a>
                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.LastLogin}</td>
                    <td className='line_table r'>{affiliateInfo.LastLoginDateText}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.LastLoginIp}</td>
                    <td className='line_table r'>
                        <a target='_blank' href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${affiliateInfo.LastLoginIp}&j=1`}>
                            {affiliateInfo.LastLoginIp}
                        </a>
                    </td>
                </tr>
                {
                    isApproved && this._renderApprovedStatusInfo()
                }
            </React.Fragment>
        )
    }
}

function mapStateToProps({ affiliateInfo, languageList }) {
    return Object.assign({}, { affiliateInfo, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateGeneralInfo, setLanguage, changePassword }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralInfo);