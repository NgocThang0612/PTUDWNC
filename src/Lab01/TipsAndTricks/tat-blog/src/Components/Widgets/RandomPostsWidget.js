import { useState, useEffect } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import { Link } from 'react-router-dom';
import { getRandomPosts } from '../../Services/Widgets';

const RandomPostsWidget = () => {
    const [postList, setPostList] = useState([]);

    useEffect(() => {
        getRandomPosts().then(data => {
            if (data)
                setPostList(data);
            else
                setPostList([]);
        });
    }, [])

    return (
        <div className='mb-4'>
            <h3 className='text-success mb-2'>
                Top 5 bài viết ngẫu nhiên
            </h3>
            {postList.length > 0 &&
                <ListGroup>
                    {postList.map((item, index) => {
                      return (
                        <ListGroup.Item key={index}>
                            <Link to={`/blog/post?${item.urlSlug}`}
                              title={item.description}
                              key={index}>
                              {item.title}
                              <span>&nbsp;({item.viewCount})</span>
                            </Link>
                        </ListGroup.Item>
                      );
                    })}
                </ListGroup>
            }
        </div>
    );
}
export default RandomPostsWidget;