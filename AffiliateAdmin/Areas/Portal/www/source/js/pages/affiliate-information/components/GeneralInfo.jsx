import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { updateLanguage, setLanguage } from '../actions';

import { status, language } from '../modules/helper';
import { formatDateTime, parseDateTime } from '../../../modules/utils';

export class GeneralInfo extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isChangingLanguage: false
        }
    }

    handleChangeLanguage = async (event) => {
        const isConfirmed = window.confirm("Are you sure you want to change language for this affiliate?");
        if (isConfirmed) {
            const language = event.target.value;
            const response = await this.props.updateLanguage(language);

            if (response.data.IsSuccessful) {
                this.props.setLanguage(language);
            }
            else{
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

    handleChangePassword = () => {
        window.parent.fContent.ibcWnd.open(url, 'Change Password', 150, 50, 350, 175, 'class1', 'class2');
    }

    _renderLanguage() {
        const { isChangingLanguage } = this.state;
        const { PreferredLanguage } = this.props;

        return (
            <React.Fragment>
                <select style={{ display: isChangingLanguage ? 'inline-block' : 'none' }}
                    defaultValue={PreferredLanguage} onChange={this.handleChangeLanguage} onBlur={this.handleBlurLanguage}>
                    {
                        Object.keys(language).map((key, index) => {
                            return (<option value={key} key={`affinfo-lang-${index}`}>{language[key]}</option>)
                        })
                    }
                </select>
                <span className='popup' style={{ display: isChangingLanguage ? 'none' : 'block' }}
                    onClick={this.clickChangeLanguage}>
                    {language[PreferredLanguage]}
                </span>
            </React.Fragment>
        )
    }

    render() {
        const { resource } = window;
        const { Status, Closed, UserName, AffId, FirstName, LastName, Gender, Phone,
            SiteUrl, LastLoginDate, LastLoginIP, InternalUrl } = this.props;

        const isApproved = Status.toLowerCase() === status.Approved;
        const siteUrl = SiteUrl.indexOf('http://') === -1 ? 'http://' + SiteUrl : SiteUrl;

        return (
            <React.Fragment>
                <tr>
                    <td style={{ width: '45%', height: 35 }}>
                        <span className='text_bold'>{resource.affiliate}</span>
                    </td>
                    <td style={{ width: '55%' }} className='r'>
                        {
                            isApproved && !Closed ?
                                (<a href={InternalUrl} className='upper b' target='_blank'>{UserName}</a>) :
                                (<span>{UserName}</span>)
                        }
                        <span> [{AffId}]</span>
                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.firstname}</td>
                    <td className='line_table r'>{FirstName}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.lastname}</td>
                    <td className='line_table r'>{LastName}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.gender}</td>
                    <td className='line_table r'>{Gender === null ? '' : (Gender === true ? resource.male : resource.female)}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.contactNumber}</td>
                    <td className='line_table r'>{Phone}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.language}</td>
                    <td className='line_table r'>
                        {this._renderLanguage()}
                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.siteUrl}</td>
                    <td className='line_table r'>
                        <a target='_blank' title={siteUrl} href={`http://anonym.to/?${siteUrl}`}>
                            {SiteUrl.replace('http://', '').replace('www.', '')}
                        </a>
                    </td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.lastLogin}</td>
                    <td className='line_table r'>{formatDateTime(parseDateTime(LastLoginDate), 'MM/dd/yyyy HH:mm:ss')}</td>
                </tr>
                <tr>
                    <td className='line_table'>{resource.lastLoginIp}</td>
                    <td className='line_table r'>
                        <a target='_blank' href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${LastLoginIP}&j=1`}>
                            {LastLoginIP}
                        </a>
                    </td>
                </tr>
                {
                    isApproved &&
                    (
                        <React.Fragment>
                            <tr style={{ height: 22 }}>
                                <td className='line_table'>{resource.password}</td>
                                <td className='r line_table'>
                                    <span className='popup'  onClick={this.handleChangePassword}>[change]</span>
                                </td>
                            </tr>
                            <tr style={{ height: 22 }}>
                                <td colSpan='2'>
                                    <a href={`../UserInfo/MemberListAll.aspx?txtAffUsername=${UserName}`} target='fMain'>{resource.memList}</a>
                                </td>
                            </tr>
                        </React.Fragment>
                    )
                }
            </React.Fragment>
        )
    }
}

function mapStateToProps(state) {
    const generalInfo = state.leftMenuData.GeneralInfo;

    const { Status, Closed, UserName, AffId, FirstName, LastName, Gender, Phone,
        PreferredLanguage, SiteUrl, LastLoginDate, LastLoginIP, InternalUrl } = generalInfo;

    return {
        Status, Closed, UserName, AffId, FirstName, LastName, Gender, Phone,
        PreferredLanguage, SiteUrl, LastLoginDate, LastLoginIP, InternalUrl
    };
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateLanguage, setLanguage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralInfo);