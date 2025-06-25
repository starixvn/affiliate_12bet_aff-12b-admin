import React, { useEffect, useState, useRef } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Modal, ModalHeader, ModalBody, Button, Form, Alert } from 'reactstrap';

import GeneralInfo from './sections/GeneralInfo';
import Personal from './sections/Personal';
import Additional from './sections/Additional';
import EmailFormat from './sections/EmailFormat';
import Payment from './sections/Payment';
import WebSite from './sections/WebSite';

import { getMarketingAccountInfo, getPaymentMethod } from '../actions';
import { toggleLoader } from './../../../actions/loaderAction';
import useNewMarketingAccountData from '../hooks/useNewMarketingAccountData';

export const AddAffMktAccountPopup = (props) => {
    const modalBodyRef = useRef();
    const {
        resultInfo,
        generalInfo, 
        setGeneralInfo, 
        personalInfo, 
        setPersonalInfo, 
        additionalInfo, 
        setAdditionalInfo, 
        paymentInfo, 
        setPaymentInfo, 
        websiteInfo, 
        setWebsiteInfo,
        handleSubmit,
    } = useNewMarketingAccountData(props.affId, props.toggleLoader);

    if(!generalInfo.hasData) {
        return null;
    }

    return (
        <Modal isOpen={props.isOpen} toggle={props.toggle} size="lg" backdrop="static" innerRef={modalBodyRef}>
            <ModalHeader toggle={props.toggle}>
                <span className="iconpopup"></span> Add a new Marketing account
            </ModalHeader>
            <ModalBody>
                <div className="modal-body-wrapper">
                    <Alert color={resultInfo.isSuccess ? 'success' : 'danger'} isOpen={resultInfo.isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: resultInfo.message }}></div>
                    </Alert>
                    <GeneralInfo generalInfo={generalInfo} setGeneralInfo={setGeneralInfo} />
                    <Personal personalInfo={personalInfo} setPersonalInfo={setPersonalInfo} />
                    <Additional additionalInfo={additionalInfo} setAdditionalInfo={setAdditionalInfo}/>
                    <EmailFormat generalInfo={generalInfo} setGeneralInfo={setGeneralInfo} />
                    <WebSite
                        websiteInfo={websiteInfo}
                        setWebsiteInfo={setWebsiteInfo}
                    />
                    <Payment
                        paymentInfo={paymentInfo}
                        setPaymentInfo={setPaymentInfo}
                        paymentMethodList={generalInfo.paymentMethods}
                        minimumPayment={generalInfo.minimumPayment}
                    />
                    <Form tag="div">
                        <Button className="mb-3" color="success" onClick={() => handleSubmit(modalBodyRef)}>SAVE</Button>
                    </Form>
                </div>
            </ModalBody>
        </Modal>
    );
};

const mapStateToProps = ({ affId }) => {
    return {
        affId
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        toggleLoader,
        getMarketingAccountInfo,
        getPaymentMethod
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddAffMktAccountPopup));