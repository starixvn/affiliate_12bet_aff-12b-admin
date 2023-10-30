import React from 'react';
import { connect } from 'react-redux';
import { Row, Col } from 'reactstrap';

import { status, resource } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import Language from './general-info/Language';
import ContactApp from './general-info/ContactApp';
import ApprovedStatusInfo from './general-info/ApprovedStatusInfo';
import ContactNumber from './general-info/ContactNumber';
import { claimResource } from '../../../../modules/claim';

export const GeneralInfo = (props) => {
    const { affiliateInfo, claimsPrincipal } = props;
    const isApproved = affiliateInfo.Status && affiliateInfo.Status.toLowerCase() === status.Approved;
    const siteUrl = affiliateInfo.SiteUrl && (affiliateInfo.SiteUrl.indexOf('http://') === -1 ? 'http://' + affiliateInfo.SiteUrl : affiliateInfo.SiteUrl);

    return (
        <div className="general-info pt-2">
            <Row className="border-0 py-1">
                <Col sm={5}>{resource.Affiliate}</Col>
                <Col sm={7} className='text-right d-inline-block'>
                    {
                       isApproved && !affiliateInfo.IsClosed ?
                            (<a href={affiliateInfo.AffLink} target='_blank'>{affiliateInfo.AffName}</a>) :
                            (<span>{affiliateInfo.AffName}</span>)
                    }{' '}
                    [<span>{affiliateInfo.AffId}</span>]
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
                <Col sm={7} className='text-right'>{affiliateInfo.Gender === null ? '' : (affiliateInfo.Gender ? resource.Male : resource.Female)}</Col>
            </Row>
            {
                claimsPrincipal.hasReadClaim(claimResource.AffPhone) &&
                <Row>
                    <Col sm={5} className="pr-0">{resource.ContactNumber}</Col>
                    <ContactNumber sm={7} className='text-right' isStatic={true}>{affiliateInfo.Phone}</ContactNumber>
                </Row>
            }
            <ContactApp />
            {
                claimsPrincipal.hasReadClaim(claimResource.AffTargetCountry) &&
                <Row>
                    <Col sm={5}>{resource.TargetCountry}</Col>
                    <Col sm={7} className='text-right'>{affiliateInfo.TargetCountryName}</Col>
                </Row>
            }
            { claimsPrincipal.hasReadClaim(claimResource.LanguageAmendment) &&
                <Row>
                    <Col sm={5}>{resource.Language}</Col>
                    <Col sm={7} className='text-right'><Language isStatic={!claimsPrincipal.hasWriteClaim(claimResource.LanguageAmendment)} /></Col>
                </Row>
            }
            { claimsPrincipal.hasReadClaim(claimResource.AffWebUrl) &&
                <Row>
                    <Col sm={5}>{resource.WebsiteUrl}</Col>
                    <Col sm={7} className='text-right text-truncate'>
                        <a target='_blank' title={htmlDecode(siteUrl)} href={`http://anonym.to/?${htmlDecode(siteUrl)}`}>
                            {affiliateInfo.SiteUrl.replace('http://', '').replace('www.', '')}
                        </a>
                    </Col>
                </Row>
            }
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
                isApproved && <ApprovedStatusInfo />
            }
        </div>
    )
}

function mapStateToProps({ leftPanel, claimsPrincipal }) {
    const { affiliateInfo } = leftPanel;
    return Object.assign({}, { affiliateInfo, claimsPrincipal });
}

export default connect(mapStateToProps)(React.memo(GeneralInfo));