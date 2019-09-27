import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setBannerDetailUrl } from '../actions/index';

export const BannerDetailPopup = (props) => {

    const { bannerDetailUrl } = props;

    const _handleClosePopup = () => {
        props.setBannerDetailUrl({ src: null })
    }

    return bannerDetailUrl && bannerDetailUrl.src && (
         <Modal isOpen={!!bannerDetailUrl.src} toggle={_handleClosePopup} size="lg" backdrop='static' style={{ width: bannerDetailUrl.width, height: bannerDetailUrl.height }}>
             <ModalHeader toggle={_handleClosePopup}><span className="iconpopup"></span></ModalHeader>
             <ModalBody>
                 <div className="modal-body-wrapper embed-responsive embed-responsive-4by3">
                     <img src={bannerDetailUrl.src} alt="" />
                 </div>
             </ModalBody>
         </Modal>
    );
}

function mapStateToProps({ bannerDetailUrl }) {
    return Object.assign({}, { bannerDetailUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setBannerDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(BannerDetailPopup));