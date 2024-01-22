import React from 'react';
import { connect } from 'react-redux';
import { Col, Row } from 'reactstrap';

import { resolveClientUrl } from '../../../modules/utils';
import { format } from '../../../modules/numberFormat';
import { claimResource } from '../../../modules/claim';

export const BannerStatistics = (props) => {

    const { generalInfo, isShowBannerLink } = props;
    const bannerNumber = format(generalInfo.BannerNumber);

    return (
        <div className="banner-statistics">
            <Row>
                <span className="tbl-icon01"></span>
                <span className="text-blue">Banner Statistics</span>
            </Row>
            <Row className="background-gray border">
                <Col sm={12}>
                    <span>We have {isShowBannerLink ? (<a href={resolveClientUrl("Portal/MediaGallery?IsFromHome=true")}><strong>{bannerNumber}</strong></a>) : (<strong>{bannerNumber}</strong>)} banners in the program</span>
                </Col>
            </Row>
        </div>
    )
}

function mapStateToProps({ generalInfo, claimsPrincipal }) {
    const isShowBannerLink = claimsPrincipal.hasReadClaim(claimResource.MediaGalleryMenu);

    return Object.assign({}, { generalInfo, isShowBannerLink });
}

export default connect(mapStateToProps)(React.memo(BannerStatistics));