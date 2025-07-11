import React, { useEffect, useState } from 'react'
import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption, setParams, setReportTitle, toggleSubmit } from '../actions';
import { viewByOptions } from '../libs/constants';
import moment from 'moment';

export const useAffiliateCpaData = (isSubmit, params, pagingOption, dispatch) => {
    const [affiliateCpaData, setAffiliateCpaData] = useState({
        mainData: null,
        total: null
    });

    useEffect(() => {
        if (isSubmit) {
            fetchData();
        }
    }, [isSubmit]);

    const fetchData = () => {
        dispatch(toggleLoader(true));
        
        fetchAffiliateCpaData()
        .then(res => {
            console.log(res);
            setAffiliateCpaData(res);
            dispatch(setPagingOption({
                ...pagingOption,
                TotalItem: res.totalItem
            }));
            dispatch(setReportTitle(getTitle()));

        }).finally(() => {
            dispatch(toggleLoader(false));
            dispatch(toggleSubmit());
        });
    }

    const getTitle = () => {
        let title = "AFFILIATE CPA REPORT";
        if(params.username) {
            title += ` - Affiliate ${params.username}`;
        }
        if(params.dealName) {
            title += ` - Deal Name ${params.dealName}`;
        }
        if(params.viewBy) {
            title += ` - View By ${viewByOptions[+params.viewBy - 1].label}`;
        }
        title += ` - From ${moment(params.fromDate).format('MM/YYYY')} - To ${moment(params.toDate).format('MM/YYYY')}`;
        
        return title;
    }

    const fetchAffiliateCpaData = async () => {
        const url = 'Portal/AffiliateCpaReport/GetData';
        const response = await post(url, params);

        return response && response.status === 200 ? response.data : null;
    }

    return affiliateCpaData
};



export default useAffiliateCpaData;


