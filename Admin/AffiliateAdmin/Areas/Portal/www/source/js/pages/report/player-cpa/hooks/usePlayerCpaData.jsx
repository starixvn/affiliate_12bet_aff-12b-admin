import { useState, useEffect } from 'react'
import { getPlayerCpaData, getTurnoverDetail } from '../libs/service'
import { toggleLoader } from '../../../../actions/loaderAction'
import { toggleSubmit, setPagingOption } from '../actions'
import { setReportTitle } from '../actions'
import { DATE_FORMAT, statusOptions, viewByOptions } from '../libs/constants'
import moment from 'moment'

const reportName = 'PLAYER CPA REPORT'

export default function usePlayerCpaData(isSubmit, params, pagingOption, memberCpaId, dispatch) {
    const [playerCpaData, setPlayerCpaData] = useState(null)
    const [turnoverDetail, setTurnoverDetail] = useState(null)
    useEffect(() => {
        if (isSubmit) {
            dispatch(toggleLoader(true))
            getPlayerCpaData(params)
            .then(data => {
                setPlayerCpaData(data)
                dispatch(setPagingOption({
                    ...pagingOption,
                    TotalItem: data?.totalItem
                }));
                dispatch(setReportTitle(getTitle()));
            })
            .catch(err => {
                console.log('err', err)
            })
            .finally(() => {
                dispatch(toggleLoader(false))
                dispatch(toggleSubmit())
            })
        }
    }, [isSubmit])

    useEffect(() => {
        if(memberCpaId) {
            dispatch(toggleLoader(true))
            getTurnoverDetail(memberCpaId)
            .then(data => {
                setTurnoverDetail(data)
            })
            .finally(() => {
                dispatch(toggleLoader(false))
            })
        }
    }, [memberCpaId])

    const getTitle = () => {
        let title = reportName;
        if(params.status){
            title += ` - Status ${statusOptions[params.status].label}`;
        }
        if(params.viewBy) {
            title += ` - View By ${viewByOptions[+params.viewBy - 1].label}`;
        }
        title += ` - From ${moment(params.fromDate).format(DATE_FORMAT)} - To ${moment(params.toDate).format(DATE_FORMAT)}`;
        
        return title;
    }


    return {
        playerCpaData,
        turnoverDetail
    }
}
