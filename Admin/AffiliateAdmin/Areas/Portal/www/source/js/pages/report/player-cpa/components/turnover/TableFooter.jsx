import React from 'react'
import { formatDecimal } from '../../../../../modules/numberFormat';

export default function TableFooter({ total }) {
  return (
    <tfoot>
        <tr style={{ backgroundColor: '#ffc4a8' }}>
            <td className="font-weight-bold" style={{ backgroundColor: '#ffc4a8' }}>Total</td>
            <td className="text-right font-weight-bold" style={{ backgroundColor: '#ffc4a8' }}>
                {formatDecimal(total.turnover)}
            </td>
            <td className="text-right font-weight-bold" style={{ backgroundColor: '#ffc4a8' }}>
                {formatDecimal(total.cpaAmount)}
            </td>
        </tr>
    </tfoot>
  )
}
