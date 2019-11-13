import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row } from 'reactstrap';

import { resolveClientUrl } from '../../../modules/utils';
import { getBannerStatistics } from '../actions';

export class BannerStatistics extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            bannerStatistics: null
        }
    }

    //componentDidMount() {
    //    this.props.getBannerStatistics();
    //}

    render() {
        const { bannerStatistics } = this.state;

        return (
            <div className="banner-statistics">
                <Row>
                            <span className="tbl-icon01"></span>
                            <h5>Banner Statistics</h5>
                    <Col sm={12}>
                        <span>We have <a href={resolveClientUrl("Portal/MediaGallery?IsFromHome=true")}>32</a> banners in the program</span>
                    </Col>
                    <Col sm={12}>
                        <span><a href={resolveClientUrl("Portal/MediaGallery?ImageType=Image&IsFromHome=true")}>23</a> image banners / <a href={resolveClientUrl("Portal/MediaGallery?ImageType=Flash&IsFromHome=true")}>9</a> flash banners</span>
                    </Col>
                </Row>
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getBannerStatistics }, dispatch);
}

export default connect(null, mapDispatchToProps)(BannerStatistics);