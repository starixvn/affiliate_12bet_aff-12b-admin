import React from 'react'
import { globalResource } from '../../../../../modules/globalResource';
import { formatDecimal } from '../../../../../modules/numberFormat';
import moment from 'moment';
import { Button } from 'reactstrap';
import { connect } from 'react-redux';
import { setMemberCpaId } from '../../actions';
import { DATE_FORMAT, InvalidDate } from '../../libs/constants';

const RowData = ({ data, rowNumber, setMemberCpaId }) => {
    const showDate = (date) => {
        return moment(date).format(DATE_FORMAT)
    }

    const showNumber = (number) => {
        return formatDecimal(number);
    }

    const handleClickTO = () => {
        setMemberCpaId(data.memberCPAId)
    }

    return (
        <tr key={`dataRow-${rowNumber}`}>
            <td className="text-center freeze-column-0">
                {rowNumber}
            </td>
            <td className="text-left freeze-column-1">
                {data.affUserName}
            </td>
            <td className="text-left freeze-column-2">
                {data.custUserName}
            </td>
            <td className="text-center">
                {data.registeredDate}
            </td>
            <td className="text-left">
                <a target='_blank' href='#'>{data.dealName}</a>
            </td>
            <td className="text-center">
                {data.scheduleName}
            </td>
            <td className="text-right">
                {data.ftd}
            </td>
            <td className="text-right">
                {data.totalDeposit}
            </td>
            <td className="text-center">
                {
                    data.isTO ? 
                        <Button 
                            className="p-0" 
                            color="link" 
                            onClick={handleClickTO}>
                            <span>Yes</span>
                        </Button> : "No"
                }
            </td>
            <td className="text-center">
                {data.aqd}
            </td>
            <td className="text-right">
                {
                    data.isTO ? 
                        <Button 
                            className="p-0" 
                            color="link" 
                            onClick={handleClickTO}>
                            <span>{data.cpaAmount}</span>
                        </Button> : data.cpaAmount
                }
            </td>
            <td className="text-center">
                {data.betTicket}
            </td>
            <td className="text-center">
                {data.hqd}
            </td>
            <td className="text-center">
                {data.hed}
            </td>
            <td className="text-right">
                {data.estimatedCPAAmount}
            </td>
            <td className="text-right">
                {data.approvedCPAAmount}
            </td>
            <td className="text-center">
                {data.cpaStatus}
                {
                    data.isAbnormal && <span title={data.description} className="ml-1 oi icon-info"></span>
                }
            </td>
            <td className="text-center">
                {data.cpaDate}
            </td>
        </tr>
    )
}

export function TableBody({
    data,
    pagingOption,
    setMemberCpaId
}) {
    if(!data || data.length === 0){
        return (
            <tbody>
                <tr>
                    <td className="text-center" colSpan={18}>{globalResource.NoInfo}</td>
                </tr>
            </tbody>
        )
    }

    const getRowNumber = (index) => {
        return (pagingOption.PageIndex - 1) * pagingOption.PageSize + index + 1;
    }

    return (
        <tbody>
            {
                data && data.map((item, index) => (
                    <RowData key={index} data={item} rowNumber={getRowNumber(index)} setMemberCpaId={setMemberCpaId} />
                ))
            }
        </tbody>
    )
}

const mapDispatchToProps = (dispatch) => {
    return {
        setMemberCpaId: (data) => dispatch(setMemberCpaId(data))
    }
}

export default connect(null, mapDispatchToProps)(TableBody)