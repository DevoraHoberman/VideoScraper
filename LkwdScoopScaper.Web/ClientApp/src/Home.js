import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Home = () => {
    const [videos, setVideos] = useState([]);
    useEffect(() => {
        const getVideos = async () => {
            const { data } = await axios.get('/api/video/scrape');
            setVideos(data);
        }
        getVideos();
    }, [])
    return <div className='container mt-5'>
        <div className='row mt-5 justify-content-center'>
            <div>
                <h1>Mostly Music Videos</h1>
                {videos.map((p, k) => {
                    return (
                        <div key={k} className='card text-center mt-3 card-body bg-light mx-auto'>
                           <h1><img classname="img-thumbnail" src={p.imageURL} /></h1>
                            <h5><a href={p.link} target='_blank'>{p.title}</a></h5>                            
                            <h5>{p.isNew}</h5>
                        </div>)
                })}

            </div>
        </div>
    </div >
}
export default Home;