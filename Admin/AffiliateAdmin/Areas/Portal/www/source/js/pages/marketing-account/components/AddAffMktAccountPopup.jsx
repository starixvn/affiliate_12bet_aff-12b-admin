import React, { useEffect, useState } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Modal, ModalHeader, ModalBody, Button, Form } from 'reactstrap';

import GeneralInfo from './sections/GeneralInfo';
import Personal from './sections/Personal';
import Additional from './sections/Additional';
import EmailFormat from './sections/EmailFormat';
import Payment from './sections/Payment';
import WebSite from './sections/WebSite';

import { getMarketingAccountInfo, getPaymentMethod } from '../actions';
import { toggleLoader } from './../../../actions/loaderAction';

export const AddAffMktAccountPopup = (props) => {

    const [hasData, setHasData] = useState(false);
    const [paymentMethods, setPaymentMethods] = useState([]);
    const [minimumPayment, setMinimumPayment] = useState(0);

    const [personalInfo, setPersonalInfo] = useState(null);
    const [additionalInfo, setAdditionalInfo] = useState(null);
    const [generalInfo, setGeneralInfo] = useState(null);
    const [paymentInfo, setPaymentInfo] = useState(null);
    const [websiteInfo, setWebsiteInfo] = useState(null);

    // TODO: Need Review
    useEffect(() => {
        if (!props.affId) {
            setHasData(false);
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
            data.TargetCountryCode = 'BD';
            data.Language = 'en-IN';

            setGeneralInfo({
                targetCountryCode: data.TargetCountryCode,
                language: data.Language
            });

            setPersonalInfo({
                firstName: data.FirstName,
                lastName: data.LastName,
                gender: data.Gender || false,
                email: data.Email,
                phone: data.Phone
            });

            setAdditionalInfo({
                address: data.Address,
                city: data.City,
                state: data.State,
                postcode: data.Postcode,
                country: data.Country,
                question: data.Question,
                answer: data.Answer
            });

            setPaymentInfo({
                preferredPayment: data.PreferredPayment,
                playerUsername: data.PlayerUsername,
                moneybookersEmail: data.MoneybookersEmail,
                bankName: data.BankName,
                bankAddress: data.BankAddress,
                bankCode: data.BankCode,
                bankAccountName: data.BankAccountName,
                bankAccountNumber: data.BankAccountNumber
            });

            setWebsiteInfo({
                siteUrl: data.SiteUrl,
                moreSiteUrls: !data.MoreSiteUrls ? [] : data.MoreSiteUrls.split(';'),
                noSiteDescription: data.NoSiteDescription,
                hasWebSite: data.HasWebSite
            });

            setHasData(true);
        }
    }

    const _fetchPaymentMethod = async () => {
        const data = await props.getPaymentMethod();

        if (data) {
            setPaymentMethods(data.PaymentMethods);
            setMinimumPayment(data.MinAmount);
        }
    }

    const _handleSubmit = () => {

    }

    return props.affId && hasData && (
        <Modal isOpen={props.isOpen} toggle={props.toggle} size="lg" backdrop="static">
            <ModalHeader toggle={props.toggle}>
                <span className="iconpopup"></span> Add a new Marketing account
            </ModalHeader>
            <ModalBody>
                <div className="modal-body-wrapper">
                    <GeneralInfo generalInfo={generalInfo} setGeneralInfo={setGeneralInfo} />
                    <Personal personalInfo={personalInfo} setPersonalInfo={setPersonalInfo} />
                    <Additional additionalInfo={additionalInfo} setAdditionalInfo={setAdditionalInfo} />
                    <EmailFormat />
                    <Payment paymentInfo={paymentInfo} setPaymentInfo={setPaymentInfo} paymentMethodList={paymentMethods} minimumPayment={minimumPayment} />
                    <WebSite websiteInfo={websiteInfo} setWebsiteInfo={setWebsiteInfo} />
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