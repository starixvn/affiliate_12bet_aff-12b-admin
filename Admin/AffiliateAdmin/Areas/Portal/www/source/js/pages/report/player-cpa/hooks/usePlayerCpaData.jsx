import React, { useState, useEffect } from 'react'

export default function usePlayerCpaData(params, isSubmit) {
    const [data, setData] = useState([])
    const [generalData, setGeneralData] = useState([])
    const [activePeriodData, setActivePeriodData] = useState([])
    const [holdingPeriodData, setHoldingPeriodData] = useState([])
    const [approvalPeriodData, setApprovalPeriodData] = useState([])

    useEffect(() => {
        if (isSubmit) {
            setData([{
                playerName: 'John Doe',
                playerId: '1234567890',
                playerEmail: 'john.doe@example.com',
                playerPhone: '1234567890',
                playerAddress: '1234567890',
            }])
        }
    }, [isSubmit])

    return {

    }
}
