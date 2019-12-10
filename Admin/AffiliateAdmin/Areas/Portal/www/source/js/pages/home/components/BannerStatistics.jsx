import React from 'react';
import { connect } from 'react-redux';
import { Col, Row } from 'reactstrap';

import { resolveClientUrl } from '../../../modules/utils';
import { format } from '../../../modules/numberFormat';

export const BannerStatistics = (props) => {

    const { generalInfo } = props;

    return (
        <div className="banner-statistics">
            <Row>
                <span className="tbl-icon01"></span>
                <span className="text-blue">Banner Statistics</span>
            </Row>
            <Row className="background-gray border">
                <Col sm={12}>
                    <span>We have <a href={resolveClientUrl("Portal/MediaGallery?IsFromHome=true")}><strong>{format(generalInfo.BannerNumber)}</strong></a> banners in the program</span>
                </Col>
                <Col sm={12}>
                    <span>
                        <a href={resolveClientUrl("Portal/MediaGallery?ImageType=Image&IsFromHome=true")}><strong>{format(generalInfo.ImageBannerNumber)}</strong></a> image banners / <a href={resolveClientUrl("Portal/MediaGallery?ImageType=Flash&IsFromHome=true")}><strong>{generalInfo.FlashBannerNumber}</strong></a> flash banners
                    </span>
                </Col>
            </Row>
        </div>
    )
}

function mapStateToProps({ generalInfo }) {
    return Object.assign({}, { generalInfo });
}

export default connect(mapStateToProps)(React.memo(BannerStatistics));