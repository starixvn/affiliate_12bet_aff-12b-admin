import React from 'react';

const Panel = (props) => {
    const { heading, children, style, ...attributes } = props;
    return (
        <div id='Bleft_Parent02' {...attributes}>
            <div id='left_submenu'>
                <div id='left_tab'>
                    <ul>
                        <li><a href='javascript:;'><span>{heading}</span></a></li>
                    </ul>
                </div>
                <div id='clear'>
                </div>
                <div id='Bleft_Sub0' style={{...style}}>
                    <table className='ltable' width='100%' border='0' cellSpacing='0' cellPadding='0'>
                        <tbody>
                            {children}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}

export default Panel;