import React from 'react'
import { formatDecimal } from '../../../../../modules/numberFormat';

export const TableFooter = ({
    total,
}) => {

    if(!total){
        return null;
    }

    return (
        <tfoot>
            <tr className="total-row" style={{ backgroundColor: '#fbe5d6' }}>
                <td colSpan="7" className="text-right font-weight-bold">{"Total"}</td>
                <td className="text-right font-weight-bold">{formatDecimal(total.cpaAmount)}</td>
                <td className="text-right font-weight-bold">{formatDecimal(total.estimatedCPAAmount)}</td>
                <td className="text-right font-weight-bold">{formatDecimal(total.approvedCPAAmount)}</td>
            </tr>
        </tfoot>
    )
}


export default TableFooter;