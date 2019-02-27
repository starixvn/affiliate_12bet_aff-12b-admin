import React from 'react';

export default function Loader(props) {
    const backdrop = props.backdrop === undefined ? true : props.backdrop;

    return (
        <div className="loader" style={{ display: props.show ? 'block' : 'none' }}>
            <div className="backdrop" style={{ display: backdrop ? 'block' : 'none' }}></div>
            <div className="spinner">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
    )
}