import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { toggleSubmitButton } from '../actions';
import Title from './Title';
import { toggleLoader } from '../../../../actions/loaderAction';

export const Content = ({ isSubmit, toggleSubmitButton, toggleLoader }) => {
    const [data, setData] = useState([])
    useEffect(() => {
        if (isSubmit) {
            console.log('isSubmit', isSubmit)
            setData([{
                playerName: 'John Doe',
                playerId: '1234567890',
                playerEmail: 'john.doe@example.com',
                playerPhone: '1234567890',
                playerAddress: '1234567890',
            }, {
                playerName: 'Jane Doe',
                playerId: '1234567890',
                playerEmail: 'jane.doe@example.com',
                playerPhone: '1234567890',
                playerAddress: '1234567890',
            }])
            setTimeout(() => {
                toggleSubmitButton()
                toggleLoader(false)
            }, 3000)
        }
    }, [isSubmit])

    return (
        <div className='data-content my-2 report-content-container'>
            <Title />
            {
                data && data.map((item, index) => (
                    <div key={index}>
                        <h1>{item.playerName}</h1>
                    </div>
                ))
            }
        </div>
    )
}

const mapStateToProps = (state) => {
    return {
        isSubmit: state.isSubmit
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ toggleSubmitButton, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);