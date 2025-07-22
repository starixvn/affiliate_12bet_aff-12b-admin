import React from 'react'
import { TotalText } from '../../libs/constants';
import { formatDecimal } from '../../../../../modules/numberFormat';

export default function TableFooter({
    total,
    totalItem
}) {
    if(!totalItem){
        return null;
    }

    return (
        <tfoot>
            <tr className="total-row freeze-row">
                <td colSpan="3" className="text-left font-weight-bold freeze-column-0">{TotalText}</td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold">{formatDecimal(total.ftd)}</td>
                <td className="text-right font-weight-bold">{formatDecimal(total.totalDeposit)}</td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold">{formatDecimal(total.cpaAmount)}</td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold">{formatDecimal(total.estimatedCPAAmount)}</td>
                <td className="text-right font-weight-bold">{formatDecimal(total.approvedCPAAmount)}</td>
                <td className="text-right font-weight-bold"></td>
                <td className="text-right font-weight-bold"></td>
            </tr>
        </tfoot>
    )

}
