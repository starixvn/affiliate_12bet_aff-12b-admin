import React from 'react'

export default function TableHeader() {
  return (
    <thead>
        <tr style={{ backgroundColor: '#e9ecef' }}>
            <th className="text-center">Product Group</th>
            <th className="text-center" style={{ width: '150px' }}>Turnover</th>
            <th className="text-center" style={{ width: '150px' }}>CPA Amount</th>
        </tr>
    </thead>
  )
}
