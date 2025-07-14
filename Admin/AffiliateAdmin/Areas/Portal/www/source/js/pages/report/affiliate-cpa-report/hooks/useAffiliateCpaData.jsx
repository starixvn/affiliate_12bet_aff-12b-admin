import React, { useEffect, useState } from 'react'
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption, setReportTitle, toggleSubmit } from '../actions';
import { viewByOptions } from '../libs/constants';
import moment from 'moment';
import { fetchAffiliateCpaData } from '../libs/services';

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
        
        fetchAffiliateCpaData({ ...params, isExportExcel: false })
        .then(res => {
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
        if(params.viewBy) {
            title += ` - View By ${viewByOptions[+params.viewBy - 1].label}`;
        }
        title += ` - From ${moment(params.fromDate).format('MM/YYYY')} - To ${moment(params.toDate).format('MM/YYYY')}`;
        
        return title;
    }

    return affiliateCpaData
};



export default useAffiliateCpaData;


