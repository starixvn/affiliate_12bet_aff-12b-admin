import { useState, useEffect } from 'react'
import { getPlayerCpaData, getTurnoverDetail } from '../libs/service'
import { toggleLoader } from '../../../../actions/loaderAction'
import { toggleSubmit, setPagingOption } from '../actions'
import { setReportTitle } from '../actions'
import { viewByOptions } from '../libs/constants'

const reportName = 'PLAYER CPA REPORT';

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
        if (memberCpaId) {
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

        title += ` - View By ${viewByOptions[+params.viewBy - 1].label}`;

        title += ` - From ${params.fromDate} - To ${params.toDate}`;

        return title;
    }

    return {
        playerCpaData,
        turnoverDetail
    }
}
