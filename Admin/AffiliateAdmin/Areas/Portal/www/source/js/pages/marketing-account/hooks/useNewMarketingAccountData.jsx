import React, { useEffect, useState } from 'react';
import { extractPersonalInfo, extractAdditionalInfo, extractPaymentInfo, extractWebsiteInfo } from '../libs/service';
import { validationEmail } from '../libs/validation';
import { defaultAdditionalInfo, defaultGeneralInfo, defaultPaymentInfo, defaultPersonalInfo, defaultWebsiteInfo } from '../libs/constants';

export default function useNewMarketingAccountData(
    affId, 
    getMarketingAccountInfo, 
    getPaymentMethod, 
) {
    const [resultInfo, setResultInfo] = useState({
        isSuccess: true,
        isShowMessage: false,
        message: ''
    });

    const [generalInfo, setGeneralInfo] = useState(defaultGeneralInfo);
    const [personalInfo, setPersonalInfo] = useState(defaultPersonalInfo);
    const [additionalInfo, setAdditionalInfo] = useState(defaultAdditionalInfo);
    const [paymentInfo, setPaymentInfo] = useState(defaultPaymentInfo);
    const [websiteInfo, setWebsiteInfo] = useState(defaultWebsiteInfo);

    // Fetch marketing account data when affId changes
    useEffect(() => {
        if (!affId) {
            setGeneralInfo(prevState => ({
                ...prevState,
                hasData: false,
            }));
            return;
        }

        _fetchData();
    }, [affId]);

    // Fetch payment methods on mount
    useEffect(() => {
        _fetchPaymentMethod();
    }, []);

    const _fetchData = async () => {
        /*const data = await getMarketingAccountInfo(affId);

        if (data) {
            setPersonalInfo(extractPersonalInfo(data));
            setAdditionalInfo(extractAdditionalInfo(data));
            setPaymentInfo(extractPaymentInfo(data));
            setWebsiteInfo(extractWebsiteInfo(data));

            setGeneralInfo(prevState => ({
                ...prevState,
                hasData: true,
                targetCountryCode: 'BD',
                language: 'en-US',
                mailOption: data.MailOption,
                hasHostAffPersonalInfoPermission: data.HasHostAffPersonalInfoPermission
            }));
        }*/
    };

    const _fetchPaymentMethod = async () => {
        /*const data = await getPaymentMethod();

        if (data) {
            setGeneralInfo(prevState => ({
                ...prevState,
                paymentMethods: data.PaymentMethods,
                minimumPayment: data.MinAmount
            }));
        }*/
    };

    const _handleGetMoreSiteUrls = (moreSiteUrlsList) => {
        return moreSiteUrlsList && moreSiteUrlsList.map(siteUrl => siteUrl.trim()).join(';');
    };

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
    };

    const _handleSubmit = (modalBodyRef) => {
        const rawData = {
            ...generalInfo,
            ...personalInfo,
            ...additionalInfo,
            ...paymentInfo,
            ...websiteInfo,
        };

        const marketingAccount = _handleTrimObject(rawData);
        const moreSiteUrls = marketingAccount.hasWebSite ? _handleGetMoreSiteUrls(marketingAccount.moreSiteUrls) : '';
        marketingAccount.moreSiteUrls = moreSiteUrls;
        const validateEmail = validationEmail(marketingAccount.email, generalInfo.hasHostAffPersonalInfoPermission);

        if (modalBodyRef?.current) {
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
    };

    const _handleCloseAlert = () => {
        setResultInfo(prevState => ({
            ...prevState,
            isShowMessage: false
        }));
    };

    return {
        // State
        resultInfo,
        generalInfo,
        personalInfo,
        additionalInfo,
        paymentInfo,
        websiteInfo,
        
        // State setters
        setGeneralInfo,
        setPersonalInfo,
        setAdditionalInfo,
        setPaymentInfo,
        setWebsiteInfo,
        
        // Functions
        handleSubmit: _handleSubmit,
        handleCloseAlert: _handleCloseAlert
    };
}
