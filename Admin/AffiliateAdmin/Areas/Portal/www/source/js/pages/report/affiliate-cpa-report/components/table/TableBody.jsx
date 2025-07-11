import React from 'react'
import { globalResource } from '../../../../../modules/globalResource';
import { formatDecimal } from '../../../../../modules/numberFormat';

const RowData = ({ data, rowNumber }) => {
    return (
        <tr key={`dataRow-${rowNumber}`}>
            <td className="text-center">{rowNumber}</td>
            <td className="text-center">{data.month}</td>
            <td><a href={`/Portal/Customer?user=${data.affiliate}`}>{data.affiliate}</a></td>
            <td>{data.dealName}</td>
            <td className="text-center">{data.dealType}</td>
            <td className="text-center">{data.instantPayment ? 'Yes' : 'No'}</td>
            <td className="text-center">{`${data.quota}`}</td>
            <td className="text-right">{formatDecimal(data.cpaAmount)}</td>
            <td className="text-right">{formatDecimal(data.estimatedCPAAmount)}</td>
            <td className="text-right">{formatDecimal(data.approvedCPAAmount)}</td>
        </tr>
    )
}
export default function TableBody({
    data,
    pagingOption
}) {
    if(!data || data.length === 0){
        return (
            <tbody>
                <tr>
                    <td className="text-center" colSpan={10}>{globalResource.NoInfo}</td>
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
                    <RowData key={index} data={item} rowNumber={getRowNumber(index)} />
                ))
            }
        </tbody>
    )
}
