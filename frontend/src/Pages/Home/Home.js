import React from 'react';
import Header from '../components/Header/Header';
import StoryCard from '../components/StoryCard/StoryCard';

const Home = () => {
    // Aquí podrías agregar lógica para obtener las historias desde tu backend o estado global
    const stories = [
        { id: 1, title: 'Historia 1', author: 'Autor 1', summary: 'Resumen de la historia 1' },
        { id: 2, title: 'Historia 2', author: 'Autor 2', summary: 'Resumen de la historia 2' },
        // Agrega más historias según sea necesario
    ];

    return (
        <div>
            <Header />
            <div style={{ padding: '20px' }}>
                {stories.map(story => (
                    <StoryCard
                        key={story.id}
                        title={story.title}
                        author={story.author}
                        summary={story.summary}
                    />
                ))}
            </div>
        </div>
    );
};

export default Home;
