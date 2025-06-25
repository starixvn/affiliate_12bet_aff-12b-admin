import React, { useEffect, useState } from 'react';
import { validationEmail } from '../libs/validation';
import { defaultAdditionalInfo, defaultGeneralInfo, defaultPaymentInfo, defaultPersonalInfo, defaultWebsiteInfo } from '../libs/constants';
import { getMarketingAccountInfo, createMarketingAccount } from '../actions';

export default function useNewMarketingAccountData(
    affId, 
    toggleLoader
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

    useEffect(() => {
        if (!affId) {
            setGeneralInfo(prevState => ({
                ...prevState,
                hasData: false,
            }));
            return;
        }
        toggleLoader(true);
        _fetchData().finally(() => {
            toggleLoader(false);
        });

    }, [affId]);

    const _fetchData = async () => {
        const data = await getMarketingAccountInfo(affId);

        if (data) {
            setPersonalInfo(prev => ({
                ...prev,
                ...data.personalInfo,
            }));
            setAdditionalInfo(prev => ({
                ...prev,
                ...data.additionalInfo,
            }));
            setPaymentInfo(prev => ({
                ...prev,
                ...data.paymentInfo,
            }));
            setWebsiteInfo(prev => ({
                ...prev,
                ...data.websiteInfo,
            }));
            setGeneralInfo(prevState => ({
                ...prevState,
                ...data.generalInfo,
                hasData: true,
                groupId: data.generalInfo.groupId
            }));
        }
    };

    const _handleSubmit = async (modalBodyRef) => {
        const rawData = {
            ...generalInfo,
            ...personalInfo,
            ...additionalInfo,
            ...paymentInfo,
            ...websiteInfo,
        };
        const emailResult = validationEmail(rawData.email, true);
        if(!emailResult.isValid) {
            setResultInfo(prevState => ({
                ...prevState,
                isSuccess: false,
                isShowMessage: true,
                message: emailResult.message
            }));    
            setTimeout(_handleCloseAlert, 3000);
            return;
        }
        toggleLoader(true);
        const result = await createMarketingAccount(rawData);
        toggleLoader(false);
        if (modalBodyRef?.current) {
            modalBodyRef.current.scrollTo({ top: 0, behavior: 'smooth' });
        }
        if (!result.isSuccessful) {
            setResultInfo(prevState => ({
                ...prevState,
                isSuccess: false,
                isShowMessage: true,
                message: result.message
            }));
            setTimeout(_handleCloseAlert, 3000);
            return;
        }

        setResultInfo(prevState => ({
            ...prevState,
            isSuccess: true,
            isShowMessage: true,
            message: 'Successfully added a new marketing account'
        }));
        setTimeout(_handleCloseAlert, 3000);
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
    };
}
