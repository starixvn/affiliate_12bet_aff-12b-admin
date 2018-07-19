import React from 'react';

const GeneralInfo = (props) => {
    return (
        <React.Fragment>
            <tr>
                <td style={{ width: '45%', height: 35 }}>
                    <span className='text_bold'>affiliate }</span>
                </td>
                <td style={{ width: '55%' }} className='r'>
                    <a href='javacript:;' style={{ display: 'block' }} className='upper b'>12BETAFF001</a>
                    <span style={{ display: 'none' }}>12BETAFF001</span>&nbsp;[<span>123456</span>]
                    </td>
            </tr>
            <tr>
                <td className='line_table'>fname</td>
                <td className='line_table r'>&nbsp;fnametext - val</td>
            </tr>
            <tr>
                <td className='line_table'>lname</td>
                <td className='line_table r'>&nbsp;lname - val</td>
            </tr>
            <tr>
                <td className='line_table'>gender</td>
                <td className='line_table r'>&nbsp;gender - val</td>
            </tr>
            <tr>
                <td className='line_table'>
                    number
                    </td>
                <td className='line_table r'>
                    &nbsp;number - val
                    </td>
            </tr>
            <tr>
                <td className='line_table'>
                    mobile
                    </td>
                <td className='line_table r'>
                    &nbsp;mobile - val
                    </td>
            </tr>
            <tr>
                <td className='line_table'>
                    language}
                    </td>
                <td className='line_table r'>
                    <select style={{ display: 'none' }} defaultValue='en-US'>
                        <option value='en-US'>English</option>
                        <option value='en-GB'>English_Euro</option>
                        <option value='zh-TW'>Chinese Traditional</option>
                        <option value='zh-CN'>Chinese Simplified</option>
                        <option value='th-TH'>Thai</option>
                        <option value='es-ES'>Spanish</option>
                        <option value='vi-VN'>Vietnamese</option>
                        <option value='id-ID'>Indonesian</option>
                        <option value='ko-KR'>Korean</option>
                        <option value='ja-JP'>Japanese</option>
                        <option value='hi-IN'>Republic of India</option>
                    </select>
                    <span className='popup'>language - val}</span>
                </td>
            </tr>
            <tr>
                <td className='line_table'>
                    siteurl}
                            </td>
                <td className='line_table r'>
                    &nbsp;<a target='_blank' title='{siteurl-val}' href='{WebsiteCheckerURL-val}'>siteurlorg-val}</a>
                </td>
            </tr>
            <tr>
                <td className='line_table'>
                    lastlogin}
                    </td>
                <td className='line_table r'>
                    &nbsp;lastlogin - val}
                    </td>
            </tr>
            <tr>
                <td className='line_table'>
                    lastloginip}
                    </td>
                <td className='line_table r'>
                    <a target='_blank' href='{LastLoginIPCheckerURL-val}'>
                        &nbsp;lastloginip - val}</a>
                </td>
            </tr>
            <tr style={{ height: 22 }}>
                <td className='line_table'>
                    pass}
                    </td>
                <td className='r line_table'>
                    <span className='popup'>
                        [change}]</span>
                </td>
            </tr>
            <tr style={{ height: 22 }}>
                <td colSpan='2'>
                    <a href='../UserInfo/MemberListAll.aspx?txtAffUsername={username}' target='fMain'>memberlist}</a>
                </td>
            </tr>
        </React.Fragment>
    )
}

export default GeneralInfo;