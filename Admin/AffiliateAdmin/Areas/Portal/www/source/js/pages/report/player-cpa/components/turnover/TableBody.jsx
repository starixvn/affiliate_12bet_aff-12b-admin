import React from 'react'
import { formatDecimal } from '../../../../../modules/numberFormat';

const ProductGroup = ({ group, index }) => {
    return (
        <React.Fragment key={index}>
            <tr style={{ backgroundColor: '#f2f2f2' }}>
                <td className="font-weight-bold">{`Group ${index + 1}`}</td>
                <td className="text-right font-weight-bold">{formatDecimal(group.turnover)}</td>
                <td className="text-right font-weight-bold">{formatDecimal(group.cpaAmount)}</td>
            </tr>
            {group.products.map((product, prodIndex) => (
                <tr key={`${index}-${prodIndex}`}>
                    <td className="pl-4">{product.productName}</td>
                    <td className="text-right">{formatDecimal(product.turnover)}</td>
                    <td className="text-right"></td>
                </tr>
            ))}
        </React.Fragment>
    )
}

export default function TableBody({ groups }) {
    return (
        <tbody>
            {
                groups.map((group, index) => (
                    <ProductGroup key={index} group={group} index={index} />
                ))
            }
        </tbody>
    )
}
