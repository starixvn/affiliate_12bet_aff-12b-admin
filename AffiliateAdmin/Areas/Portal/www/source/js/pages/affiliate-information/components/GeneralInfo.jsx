import React from 'react';
import { connect } from 'react-redux';

import { status, language } from '../modules/helper';
import { formatDateTime, parseDateTime } from '../../../modules/utils';

export class GeneralInfo extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChangeLanguage = (event) => {
        console.log(event.target.value);
    }

    render() {
        const { resource } = window;
        const { Status, Closed, UserName, AffId, FirstName, LastName, Gender, Phone, 
            PreferredLanguage, SiteUrl, LastLoginDate, LastLoginIP, InternalUrl } = this.props;

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
                        <select style={{ display: 'none' }} defaultValue={PreferredLanguage} onChange={this.handleChangeLanguage}>
                            {
                                Object.keys(language).map((item, index) => {
                                    return (<option value={item} key={`affinfo-lang-${index}`}>{language[item]}</option>)
                                })
                            }
                        </select>
                        <span className='popup'>{language[PreferredLanguage]}</span>
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
                                    <span className='popup'>[change]</span>
                                </td>
                            </tr>
                            <tr style={{ height: 22 }}>
                                <td colSpan='2'>
                                    <a href='../UserInfo/MemberListAll.aspx?txtAffUsername={username}' target='fMain'>{resource.memList}</a>
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

export default connect(mapStateToProps)(GeneralInfo);