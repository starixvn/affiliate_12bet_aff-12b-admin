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
import { extractPersonalInfo, extractAdditionalInfo, extractPaymentInfo, extractWebsiteInfo } from '../libs/service';
import { validationEmail } from '../libs/validation';
import useNewMarketingAccountData from '../hooks/useNewMarketingAccountData';

export const AddAffMktAccountPopup = (props) => {

    const modalBodyRef = useRef();

    const [resultInfo, setResultInfo] = useState({
        isSuccess: true,
        isShowMessage: false,
        message: ''
    });

    const { generalInfo, setGeneralInfo, personalInfo, setPersonalInfo, additionalInfo, setAdditionalInfo, paymentInfo, setPaymentInfo, websiteInfo, setWebsiteInfo } = useNewMarketingAccountData(props.affId);

    // TODO: Need Review
    useEffect(() => {
        if (!props.affId) {
            setCommonMarketingAccount(prevState => ({
                ...prevState,
                hasData: false,
            }));
            return;
        }

        props.toggleLoader(true);

        _fetchData();

        props.toggleLoader(false);
    }, [props.affId]);

    useEffect(() => {
        _fetchPaymentMethod();
    }, []);

    const _fetchData = async () => {
        const data = await props.getMarketingAccountInfo(props.affId);
        if (data) {
            setPersonalInfo(data.personalInfo);
            setAdditionalInfo(data.additionalInfo);
            setPaymentInfo(data.paymentInfo);
            setWebsiteInfo(data.websiteInfo);
            setGeneralInfo(prevState => ({
                ...prevState,
                ...data.generalInfo,
                hasData: true
            }));
        }
    }

    const _fetchPaymentMethod = async () => {
        
    }

    const _handleGetMoreSiteUrls = (moreSiteUrlsList) => {
        return moreSiteUrlsList && moreSiteUrlsList.map(siteUrl => siteUrl.trim()).join(';');
    }

    const _handleTrimObject = (objectOriginal) => {
        const newObject = Object.keys(objectOriginal).reduce((accumulator, key) => {
            let item = objectOriginal[key];
            if (typeof item === 'string') {
                item = item.trim();
            }
            accumulator[key] = item;

            return accumulator;
        }, {});

        if (newObject.hasWebSite) {
            newObject.noSiteDescription = '';
        } else {
            newObject.siteUrl = '';
        }

        return newObject;
    }

    const _handleSubmit = () => {
        const rawData = {
            ...generalInfo,
            ...personalInfo,
            ...additionalInfo,
            ...paymentInfo,
            ...websiteInfo,
        }

        const marketingAccount = _handleTrimObject(rawData);
        const moreSiteUrls = marketingAccount.hasWebSite ? _handleGetMoreSiteUrls(marketingAccount.moreSiteUrls) : '';
        marketingAccount.moreSiteUrls = moreSiteUrls;
        const validateEmail = validationEmail(marketingAccount.email, generalInfo.hasHostAffPersonalInfoPermission);

        if (modalBodyRef.current) {
            modalBodyRef.current.scrollTo({ top: 0, behavior: 'smooth' });
        }
        if (validateEmail.isValid) {
            setResultInfo(prevState => ({
                ...prevState,
                isSuccess: true,
                isShowMessage: true,
                message: 'Successfully added a new marketing account'
            }));
            setTimeout(_handleCloseAlert, 3000);
        } else {
            setResultInfo(prevState => ({
                ...prevState,
                isSuccess: false,
                isShowMessage: true,
                message: validateEmail.message
            }));
            setTimeout(_handleCloseAlert, 3000);
        }
    }

    const _handleCloseAlert = () => {
        setResultInfo(prevState => ({
            ...prevState,
            isShowMessage: false
        }));
    }

    return generalInfo.hasData && (
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
                    <EmailFormat mailOption={generalInfo.mailOption} setMailOption={setGeneralInfo} />
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
                        <Button className="mb-3" color="success" onClick={_handleSubmit}>SAVE</Button>
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