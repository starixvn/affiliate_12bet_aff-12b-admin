import React from 'react';

const SearchBox = (props) => {
    return (
        <table cellSpacing='0' cellPadding='0' width='98%'>
            <tbody>
                <tr>
                    <td style={{ verticalAlign: 'top' }}>
                        <input className='text_f' style={{ width: 130 }} />
                        <a id='b_button' href='javacript:;'>&nbsp;</a>
                    </td>
                </tr>
            </tbody>
        </table>
    )
}

export default SearchBox;